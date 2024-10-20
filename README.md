## Задача 1

В качестве базы данных используется MS SQL Server.

Запустите `Docker` контейнер с БД перед запуском приложения с помощью следующей команды:
```bash
docker run --name mssql-testfin --hostname mssql-testfin -d -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Dev_123##" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

При запуске приложения, миграции будут автоматически запущены.

Для хранения данных создана таблица CodeValues ( Id int, Index int, Code int, Value nvarchar(max) )

При очистке и записи данных в таблицу CodeValues используется транзакция с уровнем изоляции Snapshot.

## Задача 2

*Написать запрос, который возвращает наименование клиентов и кол-во контактов клиентов*
```sql
SELECT c.ClientName, count(cc.Id) AS ContactsCount FROM Clients c
LEFT JOIN ClientContacts cc ON cc.ClientId = c.Id 
GROUP BY c.Id, c.ClientName
```

*Написать запрос, который возвращает список клиентов, у которых есть более 2 контактов*
```sql
SELECT c.ClientName, count(cc.Id) AS ContactsCount FROM Clients c
INNER JOIN ClientContacts cc ON cc.ClientId = c.Id 
GROUP BY c.Id, c.ClientName
HAVING count(cc.Id) > 2
```

## Задача 3

*Написать запрос, который возвращает интервалы для одинаковых Id.*
```sql
-- Optimized 
SELECT DISTINCT 
	p.Id, 
	p.Dt AS StartDate,
	p2.Dt AS EndDate
FROM Dates p
CROSS APPLY
	(
		SELECT top(1) Id, Dt FROM Dates p2 WHERE p2.Dt > p.Dt AND p2.Id = p.Id ORDER BY p2.Id, p2.Dt
	) p2	
ORDER BY p.Id, p.Dt 



-- Older version
WITH DatePeriods AS (
    SELECT 
        datesLower.Id, 
        datesLower.Dt AS Sd, 
        MIN(datesGreater.Dt) AS Ed
    FROM Dates datesLower
    LEFT JOIN Dates datesGreater
        ON datesGreater.Id = datesLower.Id 
        AND datesGreater.Dt > datesLower.Dt
    GROUP BY datesLower.Id, datesLower.Dt
)
SELECT Id, Sd, Ed
FROM DatePeriods
WHERE Ed IS NOT NULL
ORDER BY Id, Sd;
```

