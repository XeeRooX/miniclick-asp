# miniclick-asp
![Главная страница](https://github.com/XeeRooX/miniclick-asp/blob/master/miniclick/readmeImages/1.png "Главная")
## Установка и запуск
Для запуска Docker контейнера используйте следующие команды:
```
git clone https://github.com/XeeRooX/miniclick-asp.git
cd ./miniclick-asp
docker compose up
```
Далее в браузере перейдите по следующему адресу:
``` http://localhost:5000 ```
## Настройки контейнера
Чтобы изменить хост сайта нужно определить переменную окружения:
```
MY_HOSTNAME=127.0.0.1:5000
```