using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Dicom;
using Dicom.Imaging;
using DicomTypeTranslation;
using WeifenLuo.WinFormsUI.Docking;

namespace TemplateBuilder
{
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
                    using (var renderedImage = new DicomImage(dicom.Dataset).RenderImage())
                    {
                        var bmp = renderedImage.As<Bitmap>();

                        DicomImage = new Bitmap(bmp.Width, bmp.Height);
                        
                        using(Graphics g = Graphics.FromImage(DicomImage))
                            g.DrawImage(bmp,new Point(0,0));

                        pictureBox1.Image = DicomImage;
                    }
                    
                }
                catch (Exception ex)
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
            var node = olvFileTags.SelectedObject as TagValueNode;

            if (node != null)
                MessageBox.Show(node.Value.ToString(), node.Tag,MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
