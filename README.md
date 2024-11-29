# Wafflewaffle

![Pic](./WafflePic.png)

A simple Udon random sound player for VRChat World with volume control

Wafflewaffle はVRChat Worldで使えるシンプルなランダムサウンドプレーヤーです。オーディオクリップ一つずつ音量が設定できますので、音量がバラバラでも簡単に修正できます。

### Functions/機能

Easy import / 導入ツール付き

Volume Control / 音量調整

Global object / グローバルオブジェクトです

### Import/導入

Download and import unitypackage from release page. 

リリースページでunitypackageをダウンロードし導入します。
<br>

In folder StudioSaphir/Wafflewaffle, drag the prefab to anywhere you want. Wafflewaffle is the older version that rely on delays to make sure the value is synced, while the WafflewaffleManual uses manual syncing and is more reliable.

StudioSaphir/Wafflewaffleのフォルダからプリハブを好きなところにD&Dします。Wafflewallfeは遅延で同期を確保する方法を利用する古いバージョンで、WafflewaffleManualは手動同期によって安定性が高くなっています。
<br>

In the toolbar /StudioSaphir/WaffleImport, set the Waffle(or Wafflewaffle inside the WafflewaffleManual object) inside and Click Reload. You need to do this every time you edit your waffle.

ツールバーの/StudioSaphir/WaffleImportにさっきのWaffle(またはWafflewaffleManualオブジェクトの中にあるWafflewaffle)を入れて、Reloadをクリックします。この作業は毎回編集する時に必要となります。
<br>

Add your sound and volume (0~1) and click Import. 

サウンドを追加して音量(0~1)を設定します。設定が終わりましたら、Importをクリックします。
<br>

Now it's finished. You can use inspector to see imported values. Cross-importing data from/to both varient is also supported.

これで完成です。WaffleのInspectorで導入されたデータが見れます。WafflewaffleからManualバージョンのデータ導入とその逆方向の導入も同じ手順でできます。

### Change 3D Model/モデルの変更

Change Mesh in Mesh Filter and Materials in Mesh Renderers in inspector, resize the collider and you are good to go.

InspectorのMesh FilterでMeshを、Mesh Rendererでマテリアルを変更し、コライダーのサイズを調整すれば完成です。

### If there are any issues, please post in the Issues page.

### もし問題がありましたら、Issuesに書いていただけると助かります。