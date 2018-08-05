# EFCoreDAL - Data access layer on entity framework core 2.1.1

## Описание тестовой предметной области:

Абстрактный базовый класс Document, с коллекцией вложенных файлов (Attachments) и одним наследником OtherDocument.
OtherDocument, конкретный класс, имеющий 2 коллекции: Items, Payments.

Основная цель проекта - реализовать следующее:

- [ ] Table per type

- [ ] Complex types

- [ ]  ~~~Lazy load~~~

- [ ] Eager load

- [ ] Specifications in access layer

- [ ] Fetch / Update strategy

- [ ] Caching in Redis
