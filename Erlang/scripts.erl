-module(scripts).
-export([send/2, stop/1, client/3, service/3]).

send(Pid, Msg) ->
    Pid ! {send, Msg}.

stop(Pid) ->
    Pid ! stop.

client(SessionID, Env, _Input) ->
    {ok, Socket} = gen_tcp:connect("localhost", 5555, [binary, {active, true}, {packet, raw}]),
    gen_tcp:send(Socket, ["" | format(Env)]),
    loop(Socket, SessionID).

loop(Socket, SessionID) ->
    receive
         {send, Msg} ->
                gen_tcp:send(Socket, Msg),
                loop(Socket, SessionID);
         {tcp, Socket, Msg} ->
		mod_esi:deliver(SessionID, ["Content-Type: text/html; charset=utf-8\r\n\r\n", Msg]),
		%io:fwrite("Send: ~p ~n", [Msg]),
                gen_tcp:close(Socket);
         stop ->
                gen_tcp:close(Socket)
         after 200 ->
                loop(Socket, SessionID)
    end.

service(SessionID, _Env, _Input) -> mod_esi:deliver(SessionID, [ 
   "Content-Type: text/html\r\n\r\n", "<html><body>Hello, World!</body></html>" ]).

format([]) ->
    "";

format([{Key, Value} | Env]) ->
    [io_lib:format("[~p: ~p]<br/>\~n", [Key, Value]) | format(Env)].