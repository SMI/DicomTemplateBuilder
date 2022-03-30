﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BrightIdeasSoftware;
using FellowOakDicom;
using DicomTypeTranslation;
using FellowOakDicom.Imaging;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Png;
using System.Runtime.Versioning;

namespace TemplateBuilder;

[SupportedOSPlatform("windows7.0")]
public partial class DicomFileTagsUI : UserControl
{
    private readonly FileInfo _fileInfo;
    private Bitmap DicomImage;
        
    public DicomFileTagsUI(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
        InitializeComponent();
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
            
        olvFileTags.ClearObjects();
            
        try
        {
            var dicom = DicomFile.Open(_fileInfo.FullName,FileReadOption.ReadAll);

            try
            {
                using var renderedImage = new DicomImage(dicom.Dataset).RenderImage().AsSharpImage();
                using MemoryStream ms = new();
                renderedImage.Save(ms,renderedImage.GetConfiguration().ImageFormatsManager.FindEncoder(PngFormat.Instance));
                ms.Seek(0, SeekOrigin.Begin);
                DicomImage = new(ms);
                pictureBox1.Image = DicomImage;
            }
            catch (Exception)
            {
                //no picture
                splitContainer1.Panel1Collapsed = true;
            }

            foreach (DicomItem item in dicom.Dataset)
            {
                var value = DicomTypeTranslater.Flatten(DicomTypeTranslaterReader.GetCSharpValue(dicom.Dataset, item));
                
                olvFileTags.AddObject(new TagValueNode(item.Tag, value));
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString(), "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

            
            
            
    }

    private void tbFilter_TextChanged(object sender, EventArgs e)
    {
        olvFileTags.ModelFilter = new TextMatchFilter(olvFileTags,tbFilter.Text);
        olvFileTags.UseFiltering = !string.IsNullOrWhiteSpace(tbFilter.Text);
    }

    private void olvFileTags_ItemActivate(object sender, EventArgs e)
    {
        if (olvFileTags.SelectedObject is TagValueNode node)
            MessageBox.Show(node.Value.ToString(), node.Tag,MessageBoxButtons.OK,MessageBoxIcon.Information);
    }
}