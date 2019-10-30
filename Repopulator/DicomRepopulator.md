# Dicom Repopulator

## Contents

- [Background](#background)
- [Matching Rows to Files](#matching-rows-to-files)
  - [By File Path](#by-file-path)
    - [File Path Formats](#file-path-formats)

## Background

The role of this component is to overwrite DicomTags in Dicom images with values provided in a CSV file.  Csv data can be at various granularities e.g. 1 row per Study or 1 row per Image.

## Matching Rows to Files

Each row in the CSV is responsible for updating one or more Dicom images.  There are three ways in which dicom files can be located:

 - By file path
 - By SopInstanceUID
 - By secondary UID (SeriesInstanceUID or StudyInstanceUID)

### By File Path

You can specify which image to anonymise directly in the CSV by creating a column called `RelativeFileArchiveURI`.  For example

```
RelativeFileArchiveURI,PatientID,StudyDate
c:\temp\mydicom.dcm,ABC123,20010101
````
_Example CSV that would write mydicom.dcm to the output directory with replaced PatientID and StudyDate values_



#### File Path Formats

When specifying the file paths (to dicom files) in the anonymisation CSV you can use either relative or absolute.

|Format|Example| Supported |
|------|-------|------------|
|Absolute Drive|c:\temp\fish.dcm|Yes|
|Absolute Network| \\\\myserver\my.dcm|Yes|
|Absolute Linux|/usr/my.dcm|Yes|
|Relative Linux (dot prefix)|.\temp\fish.dcm|Yes|
|Relative Windows(dot prefix)|./temp/fish.dcm|Yes|
|Relative No Prefix|temp/fish.dcm|No|

## Arbitrary Column Names (and 1-to-n mapping)

Normally columns in the CSV are expected to be named after dicom tags e.g. PatientID.  However you can use your own headers (e.g. patid) by supplying an `Extra Mappings` file.

Extra mappings are specified in the following format:

```
SomeCsvColumn:DicomTagName
```

Extra mappings can be used to populate multiple tags by specifying the same csv column multiple times:

```
MyDate:StudyDate
MyDate:SeriesDate
```
_Example would write values stored in the MyDate column of the CSV into dicom tags StudyDate and SeriesDate_


## Outstanding Issues

- Sequences
  - Populate an entire sequence
  - Dive into sequence and populate only a given leaf
- Multiplicity
- CLI version?
- Use case, you want arbitrary file paths in CSV into a large repository of images (millions) and don't want to do the `dir *.dcm recursive` just to get the count of files to process (applies only to `FilePathMatcher`)

- Null behaviour? What happens when the csv has no value in a cell do we skip the tag or remove it?

## Error states

- Csv file path is to a file not in Input Directory (means counts arent accurate)
- File on disk has UID not in CSV
- CSV has UIDs not on disk (warning?)

- Files do not exist
- Hidden files e.g. thumbs.db especially when processing dicoms that lack extensions (apparently .dcm is optional in dicom format)

- Two+ rows in CSV with same UID e.g. SeriesInstanceUID when no SOPInstanceUID 

- Overwritting of same file with different answer e.g. CSV file has path in two places in it


