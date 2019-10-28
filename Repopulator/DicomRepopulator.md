# Dicom Repopulator

## Background

The role of this component is to overwrite DicomTags in Dicom images with values provided in a CSV file.

## Matching Rows to Files

Each row in the CSV is responsible for updating one or more Dicom images.  There are three ways in which dicom files can be located:

 - By file path
 - By SopInstanceUID
 - By secondary UID (SeriesInstanceUID or StudyInstanceUID)

### By File Path

