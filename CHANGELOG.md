# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

_No changes yet._

## [0.2.0-alpha] - 2025-11-17

### Added
- Enforced XML documentation (CS1591) for all public APIs and added `DocumentationCoverageTests` to guard regressions.
- Introduced `examples/Max.Bot.Examples` console project with Echo/Command/Keyboard/File bot samples inspired by Telegram.Bot and VkNet.
- Added comprehensive smoke tests for the new samples (`SampleBotsTests`) leveraging mocked runtimes.
- Expanded README with installation steps, quick start, feature overview, and sample catalogue plus references to official docs and sibling SDKs.

### Changed
- Updated `Directory.Build.props` to treat missing XML docs as errors while exempting the test project.
- Refined solution structure to include the examples project for local experimentation.

## [0.1.0-alpha] - 2025-01-XX

### Added
- Initial project setup
- Basic infrastructure and folder structure
- Build configuration for .NET 9

[Unreleased]: https://github.com/YOUR_REPO/MaxBotNet/compare/v0.2.0-alpha...HEAD
[0.2.0-alpha]: https://github.com/YOUR_REPO/MaxBotNet/compare/v0.1.0-alpha...v0.2.0-alpha
[0.1.0-alpha]: https://github.com/YOUR_REPO/MaxBotNet/releases/tag/v0.1.0-alpha

