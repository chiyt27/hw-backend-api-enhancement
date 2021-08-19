## 必要條件
<ol>
	<li>Framework 使用 DotNet Core 3.1</li>
	<li>引用套件：
		<ul>
			<li>Swagger：提供API測試介面</li>
			<li>Newtonsoft.Json：序列化工具</li>
			<li>Dapper：ORM工具</li>
		</ul>  
	</li>
	<li>DB 使用 SQLite</li>
</ol>

## 說明
### DB schema
```
CREATE TABLE [bill_detail] (
	[bill/PayerAccountId] nvarchar(20) NOT NULL, 
	[lineItem/UnblendedCost] decimal(15, 10), 
	[lineItem/UnblendedRate] decimal(15, 10), 
	[lineItem/UsageAccountId] nvarchar(20) NOT NULL, 
	[lineItem/UsageAmount] decimal(15, 10), 
	[lineItem/UsageStartDate] timestamp, 
	[lineItem/UsageEndDate] timestamp, 
	[product/ProductName] nvarchar(254)
);
CREATE INDEX UsageAccountidIndex ON bill_detail (
    "lineItem/UsageAccountId"
);
```
<img src="https://github.com/chiyt27/pic-repo/blob/master/hw-backend-api-enhancement/sqlite%E6%88%AA%E5%9C%96.png?raw=true">

### 程式說明
<ol>
	<li>appsettings.Development.json 內指定 SQLite 檔案。</li>
	<li>內含功能：</li>
</ol>
		
| 功能API | 用途說明 |
|--|--|
| /api​/Billing | 查詢全部的 table 資料 |
| /api/Billing/{limit}/{offset} | 增加分頁資訊去查詢 table 資料 |
| /api​/Billing​/getCostSumByAccountId​/{accountid} | 輸入UsageAccountId，根據ProductName分組計算出 Cost 總和 |
| /api​/Billing​/getDailyAmountByAccountId​/{accountid} | 輸入UsageAccountId，根據ProductName分組列出每日的資源使用量(UsageAmount) |

<img src="https://github.com/chiyt27/pic-repo/blob/master/hw-backend-api-enhancement/swagger%E6%88%AA%E5%9C%96.png?raw=true">


###  <font color="red">未完成功能/需加強項目</font>
<ol>
	<li>應該開放API介面上傳 csv 將資料送入DB。</li>
	<li>SQLite目前撈資料很常發生 DB 資料無法轉換回 decimal。</li>
	<li><b><i>Target.3.ii</i></b> (文件要求取得每日資源使用量功能) 的資料是寫死的，不是來自DB。</li>
	<li>發生例外或錯誤情況時的 response 格式需處理。</li>
	<li>尚未處理效能議題。</li>
	<li>應該加入log。</li>
</ol>

