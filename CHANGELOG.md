# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Added logging to ProcessJob for outPath in DicomRepopulatorProcessor

### Dependency updates

- Bump CsvHelper from 19.0.0 to 21.0.4

### Changes

- LGTM fixes
- Report if error encountered when saving or loading a file

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


[Unreleased]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.1.0...develop
[1.1.0]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.2...v1.1.0
[1.0.2]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/HicServices/DicomTemplateBuilder/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/HicServices/DicomTemplateBuilder/tree/v1.0.0
[DicomTypeTranslation]: https://github.com/HicServices/DicomTypeTranslation
