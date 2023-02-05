# process-communication
write C# or cs file to imply variable ways communication between different processes.
本项目根据unix第二卷进程间通讯写成


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
|使用类||MessageQueue|
|文件实例||[msmqsend](./MSMQ/msmqsend.cs)|

在window操作系统上，提供了MSMQ（Microsoft Message Queuing）的消息队列服务，而不是.NET生态提供的组件。
当然了，消息队列和管道由于都是在内核中，所以在一次消息发送和接收过程中，都会经过俩次的用户态和内核态的消息交换。这部分还是会造成效率不高。


## 共享内存
为了解决用户态到内核态的消息拷贝的效率问题，提出了共享内存的方法。
在进程的虚拟进程中，将一部分虚拟地址映射成物理磁盘文件或临时内存，其他要通信的进程，也将一部分虚拟地址映射成该相同的磁盘文件或临时内存。这样当一个进程在操作此虚拟地址的时候，实际上是操作该磁盘文件或临时内存，而另外一个进程也能看到。与消息队列和命名管道一样，可不限于父子关系的进程。

|类型|临时共享内存|持久共享内存(映射磁盘文件)|
|----|----|----|
|生存范围|最后一个进程操作后，由操作系统回收|最后一个进程操作后，持久化到文件系统中|
|使用类|MemoryMappedFile|MemoryMappedFile|
|文件实例|[tempstream](./Share/tempStreamA.cs)|[fileaccess](./Share/fileAccessCreate.cs)|

此处使用可参考MSDN的视图原理。[MSDN](https://learn.microsoft.com/zh-cn/dotnet/standard/io/memory-mapped-files)
