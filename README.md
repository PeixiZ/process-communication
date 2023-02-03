# process-communication
write C# or cs file to imply variable ways communication between different processes.
本项目根据unix第二卷————进程间通讯写成

## Pipes(管道)
根据定义，管道分为匿名管道和命名管道，匿名管道适用于有明确父子关系的进程间传递信息，命名管道可用于不同进程间传递信息，即不限于父子关系。

|类型|匿名管道|命名管道|
|----|----|----|
|使用范围/区别|只能父子进程之间|不限父子关系|
|使用类|AnonymousPipeServerStream;AnonymousPipeClientStream | NamePipeClientStream;NamePipeServerStream|
|文件实例|[anonymousClient](./Pipes/pipeClient.cs);  [anonymousServer](./Pipes/pipeServer.cs)|[nameclient](./Pipes/namePipeClient.cs);  [nameServer](./Pipes/namePipeServer.cs)|

此处需要说明的是，管道的单向是对于进程本身来说，进程在创建管道的时候，会返回管道两端的描述符，因此进程可以指定管道两端中哪一端是读，哪一端是写，而对于另外一个进程来说却不影响，也就是另外一个进程可以重新选择管道两端哪一端是读或写，可以与第一个进程相反。无论是匿名还是命名管道，都是半双工方式，可以使用两个或者多个管道，形成进程间的全双工管道。

## 消息队列
消息队列与管道的不同之处在于，进程可以在发送消息之后立刻返回，另外一个进程可以在以后的某个时刻去读取该消息队列。

|类型|管道|消息队列|
|----|----|----|
|生存范围|随进程结束即结束（进程退出）|随内核结束即结束（操作系统退出）|

在window操作系统上，提供了MSMQ（Microsoft Message Queuing）的消息队列服务，而不是.NET生态提供的组件
