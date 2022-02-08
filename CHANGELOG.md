# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.1.0] - 2022-02-08

### Changes

- Using .Net 5.0

### Dependency updates

- Bump CsvHelper from 26.1.0 to 27.2.1
- Bump HIC.DicomTypeTranslation from 2.3.2 to 3.0.0
- Bump DockPanelSuite from 3.1.0-beta7 to 3.1.0
- Bump fo-dicom from 4.0.7 to 4.0.8
- Bump fo-dicom.Drawing from 4.0.7 to 4.0.8
- Bump HIC.DicomTypeTranslation from 2.3.2 to 3.0.0
- Bump System.Drawing.Common from 5.0.2 to 6.0.0
- Bump fernandreu.ScintillaNET from 4.0.4 to 4.2.0


## [2.0.0] - 2021-03-27

### Added

- Added logging to ProcessJob for outPath in DicomRepopulatorProcessor

### Dependency updates

- Bump CsvHelper from 19.0.0 to 23.0.0

### Changes

- LGTM fixes
- Report if error encountered when saving or loading a file
- Updated to dotnet 5

### Added

- DeleteAsYouGo mode where input files are deleted after processing to the output directory

## [1.1.0] - 2020-02-07

### Added

- Updated to dotnet core 3.1 CLI version of Repopulator

## [1.0.2] - 2020-02-06

### Added

- New option for DicomRepopulator to put images in root subfolders e.g. by PatientID
- Added Stop button for DicomRepopulator to cancel processing

### Changed

- Improved GUI usability 

## [1.0.1] - 2019-12-09

### Added 

- Added initial iteration of dicom tag repopulator (anonymises dicom images based on CSV data)
- Updated [DicomTypeTranslation] to 2.1.2

## [1.0.0] - 2019-10-01

Initial version


[Unreleased]: https://github.com/HicServices/DicomTemplateBuilder/compare/v2.1.0...develop
[2.1.0]: https://github.com/HicServices/DicomTemplateBuilder/compare/v2.0.0...v2.1.0
[2.0.0]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.1.0...v2.0.0
[1.1.0]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.2...v1.1.0
[1.0.2]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/HicServices/DicomTemplateBuilder/tree/v1.0.0
[DicomTypeTranslation]: https://github.com/HicServices/DicomTypeTranslation
