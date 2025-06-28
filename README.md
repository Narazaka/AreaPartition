# Area Partition

Automatically generates grid-based area partitions for VRChat worlds etc., with support for occlusion culling meshes and colliders.

VRChatワールドなど向けに、Occlusion Culling用メッシュと衝突を備えたグリッド状のエリア分割を自動生成するツール。

## Install

### OpenUPM

See [OpenUPM page](https://openupm.com/packages/net.narazaka.unity.area-partition/)

### VCC用インストーラーunitypackageによる方法（おすすめ）

https://github.com/Narazaka/AreaPartition/releases/latest から `net.narazaka.unity.area-partition-installer.zip` をダウンロードして解凍し、対象のプロジェクトにインポートする。

### VCCによる方法

1. https://vpm.narazaka.net/ から「Add to VCC」ボタンを押してリポジトリをVCCにインストールします。
2. VCCでSettings→Packages→Installed Repositoriesの一覧中で「Narazaka VPM Listing」にチェックが付いていることを確認します。
3. アバタープロジェクトの「Manage Project」から「Area Partition」をインストールします。

## Usage

`AreaPartitionGenerator` プレハブをシーンに配置するか、空のGameObjectに `Area Partition Generator` コンポーネントを追加する。

`Room` プレハブのVariantとしてエリアのテンプレートを作り、`Room Settings`に設定し数を決める。

`Occlusion` のチェックを入れるとOcclusion Culling用メッシュが表示されるので、その状態でOcclusion Cullingをベイクして下さい。

### 注意: Occlusion Cullingの暴発

原点から遠い場所に領域がある場合、Occlusion Cullingの精度が悪くなり、変なカリングが為されてしまうことがあります。

この場合位置を原点に寄せるか、Area Partition GeneratorのBoundsを大きく調整してからもう一回「Regenerate Rooms」と「Setup」をし、その状態でOcclusion Cullingをベイクするなどで改善することがあります。

## 更新履歴

- 1.1.2: Occlusion Enabledの値が保持されるように
- 1.1.1: fix Missing Script
- 1.1.0: AutoGenerate / Root を追加
- 1.0.0: リリース

## License

[Zlib License](LICENSE.txt)
