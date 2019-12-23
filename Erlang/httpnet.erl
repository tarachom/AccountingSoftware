-module(httpnet). 
-export([start/0,service/3]). 

start() ->
   inets:start(httpd, 
[
    {modules, [
        mod_alias,
        mod_auth,
        mod_esi,
        mod_actions,
        mod_cgi,
        mod_get,
        mod_head,
        mod_dir,
        mod_log,
        mod_disk_log
    ]},

    {port, 80},
    {bind_address, "127.0.0.1"},
    {server_name, "httpnet"},

    {server_root, "C://HttpNet"},
    {document_root, "C://HttpNet/htdocs"},
    {directory_index, ["index.html"]},

    {erl_script_alias, {"/erl", [scripts]}},

    {error_log, "error.log"},
    {security_log, "security.log"},
    {transfer_log, "transfer.log"},

    {mime_types, [
        {"html", "text/html"},
        {"css", "text/css"},
        {"js", "application/x-javascript"}
    ]},
    {mime_type, "application/octet-stream"}
]).

