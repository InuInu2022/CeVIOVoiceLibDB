# CeVIO voice libraries database



----
[![MIT License](http://img.shields.io/badge/license-MIT-blue.svg?style=flat)](LICENSE) ![GitHub Repo stars](https://img.shields.io/github/stars/InuInu2022/CeVIOVoiceLibDB?label=%E2%98%85&logo=github)
[![CeVIO CS](https://img.shields.io/badge/CeVIO_Creative_Studio-7.0-d08cbb.svg?logo=&style=flat)](https://cevio.jp/) [![CeVIO AI](https://img.shields.io/badge/CeVIO_AI-8.6-lightgray.svg?logo=&style=flat)](https://cevio.jp/) [![VoiSona](https://img.shields.io/badge/VoiSona-1.7-53abdb.svg?logo=&style=flat)](https://voisona.com/)

音声合成ソフト[CeVIO](https://cevio.jp/) / [VoiSona](https://voisona.com/)の音源（ボイスライブラリ）比較用リポジトリです。

<!--
## DEMO
-->

## 📦Features

* CeVIO AIのトークボイス・ソングボイス全比較:smile:
* VoiSonaのソングボイス全比較:smile:

## Requirement

以下のソフトの最新版が必要です。

* Windows
  * CeVIO AI
  * CeVIO CS
* Windows / macOS
  * VoiSona

また各ボイスライブラリも必要です。

## Installation

解析ツールやデータ作成ツールを使用するのに使います。

[tools](./tools/)ディレクトリ以下に各種ツールがあります。

* .NET SDK
* ライブラリ(submodule)
  * cevio-casts : CeVIO / VoiSona 全キャストデータ
  * LibSasara : ccsファイル編集ライブラリ

## Usage

### 🧪Song voice data

#### [Song] AlphaValueCheck

* ソングボイスのAlpha（声質バー）の最大と最小のチェックデータです

#### [Song] DefaultTmgCheck

* ソングボイスのデフォルトの推定TMGのデータです
* ccsファイルとlabファイルが含まれます
* :construction_worker:WIP

#### [Song] DynamicsCheck

* :construction_worker:WIP
* ソングボイスの強弱指定の影響のデータです

#### [Song] KeyTmgCheck

* :construction_worker:WIP
* ソングボイスの調号指定の違いによる影響のデータです
* ccsファイルとlabファイルが含まれます

#### [Song] SpecialLabelCheck

* :construction_worker:WIP
* CeVIO / VoiSonaの歌詞中に入力可能な特殊記号のチェックデータです。

#### [Song] VoiSonaCeVIOHuskeyCheck [![CeVIO AI](https://img.shields.io/badge/CeVIO_AI-8.5-lightgray.svg?logo=&style=flat)](https://cevio.jp/)

* CeVIO / VoiSona両方でリリースされているボイスライブラリのHuskeyの値のチェックデータです。

#### [Song] WhisperCheck [![CeVIO AI](https://img.shields.io/badge/CeVIO_AI-8.5-lightgray.svg?logo=&style=flat)](https://cevio.jp/)

* :construction_worker:WIP
* CeVIO AI 8.5以降でささやき歌唱を再現するデータです
* VOLやHuskyなどの値がキャストによって異なるので手動で検証しています

### 🧪Talk voice data

:construction_worker:WIP

## 📓Note

## :dog:Author

* InuInu（いぬいぬ）
  * [YouTube](https://bit.ly/InuInuMusic)
  * [@InuInuGames](https://twitter.com/InuInuGames)
  * [note.com](https://note.com/inuinu_)
  * niconico [niconico](https://nico.ms/user/98013232)

## License

"CeVIO voice libraries database" is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).

## :link:Related projects

* :octocat:[LibSasara](https://github.com/InuInu2022/LibSasara)
* :octocat:[Fluent CeVIO Wrapper](https://github.com/InuInu2022/FluentCeVIOWrapper)