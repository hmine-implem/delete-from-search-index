# delete-from-search-index
2025/04/24 記

## 概要
Azureのサービスの一つである「[Azure AI Search](https://learn.microsoft.com/ja-jp/azure/search/search-what-is-azure-search)」（以下「AI Search」）のAPIを呼び出すツールです。  
作者がAI Searchの動作検証で使用したものです。

### 対応API
- [Azure AI Search REST API](https://learn.microsoft.com/ja-jp/rest/api/searchservice/addupdate-or-delete-documents)を呼び出してインデックスを操作します。
    - api-versionにはapi-version=2024-07-01を指定しています。
- [Azure AI Searchを利用したSemantic Kernelテキスト検索(RAG)の実装](https://github.com/pleasanter-developer-community/azure-ai-rag-with-pleasanter/tree/feature/api-upload-index)で構築するインデックスの削除に特化したツールです。
  - 上記記事が最初に公開された時は[プリザンター](https://pleasanter.org/)において、一括削除時にサーバスクリプトを実行する機能がなかったため、インデックスの削除はプリザンター上からの個別削除と連動させて削除することしかできませんでした。そのため本ツールを開発しインデックスの削除作業を一括で行えるようにして作業を効率化させていました。

## 特徴
AI Searchのインデックスの「検索エクスプローラ」（★）全体をコピー＆ペーストしてinput.jsonに貼り付けておくと、そのまま「検索エクスプローラ」に表示されていた全インデックスが削除対象となります。  
```
[(1)Search Serviceの管理画面に移動]→[(2)検索管理]→[(3)インデックス]→[(3)インデックス名]→[(5)クエリオプションをクリックし「ベクトル検索」をオフ]→[(6)検索文字列を空欄にして検索実行]
```
★「検索エクスプローラ」は以下画像の「結果」枠内を表しています。    
    
![検索エクスプローラ](./img/delete-from-search-index-01.png)  

## 動作確認環境
1. Windows11
1. .NET9をインストール済み
1. VS Codeのターミナル（Ponwershell）と、VS Codeのデバッグコンソールで起動を確認済み
1. VS Codeには拡張機能の「C#」「C# Dev Kit」をインストール済み

## 操作手順
1. 事前準備①  
    Program.csの定数AiSearchUrlを書き換えます。
    - {Azure AI Search Service Name}　：　AI Searchのリソース名
    - {Index Name}　：　インデックス名
1. 事前準備②  
    Program.csの定数AiSearchKey（{Azure AI Search API Key}の部分）を書き換えます。
    ```
    [(1)Search Serviceの管理画面]→[(2)設定]→[(3)キー]→[(4)プライマリ管理者キーをクリップボードにコピー]
    ```
1. 事前準備③  
   input.jsonで削除対象データのキー値等を指定します。
   - 本ツールでは、AI Searchのインデックスの「検索エクスプローラ」の全体をコピー＆ペーストする使い方を想定しています。
1. 実行後メッセージを確認  
    APIの実行に成功するとコンソールにレスポンスのメッセージが表示されます。削除対象のデータ（全量）のキー値等が表示されます。

## 拡張方法
1. 「追加、更新」のAPI呼び出しについても「削除」との共通項目があるため、本ツールの考え方でAPI呼び出しツールを開発できます。

## 関連情報
- [Azure AI Search の概要 - Azure AI Search | Microsoft Learn](https://learn.microsoft.com/ja-jp/azure/search/search-what-is-azure-search)
- [ドキュメントの追加、更新、または削除" - Azure AI Search | Microsoft Learn](https://learn.microsoft.com/ja-jp/rest/api/searchservice/addupdate-or-delete-documents)
- [Azure AI Searchを利用したSemantic Kernelテキスト検索(RAG)の実装](https://github.com/pleasanter-developer-community/azure-ai-rag-with-pleasanter/tree/feature/api-upload-index)
