# Лабораторная №2 — Сервер ветеринарной клиники

Проект основан на исходной лабораторной №1. Сервер ASP.NET Core 10 реализует CRUD шести сущностей и пять аналитических запросов. Данные хранятся в памяти и при перезапуске восстанавливаются из датасида первой лабораторной.

## Запуск

```bash
dotnet restore VeterinaryClinic.slnx
dotnet test VeterinaryClinic.slnx
dotnet run --project VeterinaryClinic.Api
```

В режиме Development OpenAPI JSON доступен по `/openapi/v1.json`. Для ручной проверки используйте Scalar UI по адресу `/scalar` (в режиме Development).

## Маршруты

`/api/breeds`, `/api/specializations`, `/api/owners`, `/api/veterinarians`, `/api/pets`, `/api/appointments` поддерживают GET (список и по ID), POST, PUT и DELETE.

Аналитика:
- `GET /api/veterinarians/by-species/Dog`
- `GET /api/veterinarians/1/pets`
- `GET /api/breeds/1/repeated-appointments/count`
- `GET /api/owners/multiple-pets`
- `GET /api/appointments/current-month?roomNumber=101`

HTTP: 201 при создании, 200 при чтении и обновлении, 204 при удалении, 400 для неверных данных, 404 для отсутствующей записи, 409 при нарушении ссылочных связей.

## Архитектура

- Domain — существующие модели первой лабораторной.
- Application.Contracts — простые DTO и запросы (`record`).
- Application — бизнес-операции и аналитические запросы, ручное преобразование в DTO.
- Infrastructure — in-memory репозитории и наполнение начальными данными.
- Api — HTTP контроллеры и обработка ошибок.

## Ограничения

Хранение временное и не рассчитано на параллельные изменения. MySQL, RabbitMQ, Bogus и Aspire относятся к следующим этапам семестрового проекта. Для продакшена понадобится транзакционное хранилище и дополнительные проверки бизнес-ограничений.

## Проверка перед отправкой

```bash
dotnet restore VeterinaryClinic.slnx
dotnet build VeterinaryClinic.slnx
dotnet test VeterinaryClinic.slnx
dotnet run --project VeterinaryClinic.Api
```

Откройте `/scalar` на адресе запущенного API. Проверьте POST, PUT, GET и DELETE, а также пять аналитических маршрутов.

### Известные ограничения

- В этой версии данные хранятся в памяти; изменения теряются при перезапуске.
- In-memory репозитории пока не рассчитаны на конкурентные изменения.
- Тесты покрывают аналитические запросы и несколько бизнес-ограничений; отдельные автоматизированные HTTP-интеграционные тесты пока не реализованы.
- Изменение вида животного у используемой породы и специализации запрещено бизнес-правилами соответствующих сервисов.

## Сервисы приложения

CRUD и аналитические запросы распределены между шестью сервисами по сущностям. Каждый контроллер зависит только от своего сервиса. Хранилище можно заменить, реализовав `IRepository<T>`.
