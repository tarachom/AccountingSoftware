    -module(client).

    -export([start/0, start/2, send/2, stop/1, client/2]).

    start() ->
        start("localhost", 5555).

    start(Host, Port) ->
        spawn(?MODULE, client, [Host, Port]).

    send(Pid, Msg) ->
        Pid ! {send, Msg},
        send_ok.

    stop(Pid) ->
        Pid ! stop,
        stop_ok.

    client(Host, Port) ->
        io:format("Client ~p connects to ~p:~p~n", [self(), Host, Port]),
        {ok, Socket} = gen_tcp:connect(Host, Port, [binary, {active, true}, {packet, raw}]),
        gen_tcp:send(Socket, "OK"),
        loop(Socket).

    loop(Socket) ->
        receive
            {send, Msg} ->
                io:format("Client ~p send ~p~n", [self(), Msg]),
                gen_tcp:send(Socket, Msg),
                loop(Socket);
            {tcp, Socket, Msg} ->
                io:format("Client ~p got message: ~p~n", [self(), Msg]),
                loop(Socket);
            stop ->
                io:format("Client ~p closes connection and stops~n", [self()]),
                gen_tcp:close(Socket)
        after 200 ->
                loop(Socket)
        end.