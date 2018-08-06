# EFCoreDAL - Data access layer on entity framework core 2.1.1

## Описание тестовой предметной области:

Абстрактный базовый класс Document, с коллекцией вложенных файлов (Attachments) и одним наследником OtherDocument.
OtherDocument, конкретный класс, имеющий 2 коллекции: Items, Payments. Коллекция Items имеет вложеную коллекцию NestedItems, которая в свою очередь имеет коллекцию OneMoreNestedItems

-   Document

    -   Attachments[]

    -   OtherDocument 

		- Payments[]

		-  Items[]

			-  NestedItems[]

				- OneMoreNestedItems[]


Основная цель проекта - реализовать следующее:

-   [x] Table per type

-   [ ] Complex types

-   [ ] ~~~Lazy load~~~

-   [x] Eager load

-   [x] Specifications in access layer

-   [x] Fetch / Update strategy

-   [x] Caching in Redis


Не решенные проблемы:

- Текущая реализация не позволяет указать предикаты для вложенных сущностей и как следствие невозможность фильтровать по ним (only not deleted etc), вероятно для этого требуется инжектить сервисы этих сущностей и запрашивать данные через них.

Например если необходимо забрать OtherDocument с OtherDocumentItems, то сервис OtherDocument.Get() должен принимать настройки для запроса OtherDocumentItems, и в реализации использовать сервис по работе с OtherDocumentItems, после чего полученные данные передать и дополнить сущность. Таким образом, сущности с подобным поведением должны иметь методы для заполнения полей, которые запрашиваются отдельно.