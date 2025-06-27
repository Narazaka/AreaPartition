# Area Partition

Automatically generates grid-based area partitions for VRChat worlds etc., with support for occlusion culling meshes and colliders.

VRChatワールドなど向けに、Occlusion Culling用メッシュと衝突を備えたグリッド状のエリア分割を自動生成するツール。

## Install

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

## License

[Zlib License](LICENSE.txt)
