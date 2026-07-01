# Финансовый менеджер для ваших трат
## Особенности:
- Система авторизации
- Надёжное хранение пароля
- Удобный интерфейс
- Добавление и редактирование бюджета
- Добавление и удаление категорий
## Установка
1. Скачайте последнюю версию в [релизах](https://github.com/Bloodmassacre/FinanceManager/releases/latest)
2. Распакуйте архив в удобное место
3. Установите [Docker Desktop](https://docs.docker.com/desktop/setup/install/windows-install)
4. Запустите файл `start.bat`
5. Готово!

**Примечание:** если у вас ошибка при скачивании PostgreSQL, тогда в Docker Desktop перейдите в Settings > Docker Engine и измените конфигурацию:
```
{
  "builder": {
    "gc": {
      "defaultKeepStorage": "20GB",
      "enabled": true
    }
  },
  "experimental": false,
  "registry-mirrors": [
    "https://docker.m.daocloud.io",
    "https://huecker.io",
    "https://dockerhub.timeweb.cloud",
    "https://noohub.ru"
  ]
}
```
