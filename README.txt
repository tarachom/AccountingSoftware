
Програма для тестування взаємодії Erlang та програми на C# по протоколу TCP/IP

На Erlang запущений веб-сервер httpd.
Програма WebServerTestErlang.exe відкриває 5555 порт і очікує підключення.

Скріпт http://127.0.0.1/erl/scripts/client підключається до програми WebServerTestErlang.exe, 
передає параметри і отримує відповідь яку пересилає браузеру.

БРАУЗЕР (http://127.0.0.1/erl/scripts/client) <-- --> Erlang <-- --> WebServerTestErlang.exe