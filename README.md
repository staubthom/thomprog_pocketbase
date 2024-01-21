# Thomprog
This programme was created purely out of curiosity about how to create a frontend with [MudBlazor](https://mudblazor.com/) and [Pocketbase](https://pocketbase.io/). As I couldn't find anything on the net that met my requirements, I ended up programming something myself. The Thomprog is available to anyone who wants to continue using it. Documentation will follow here shortly. 
## Start Backend
  

Change to the pocketbase directory in the terminal and execute the following command:

    pocketbase.exe serve

This starts the backend

├─ REST API: http://127.0.0.1:8090/api/

└─ Admin UI: http://127.0.0.1:8090/_/

Looks like this:

![Terminalprompt](/doc/promt_pb.jpg "Terminalprompt")

For more information, see Readme in Pocketbase folder.

In the thomprog directory run:

    dotnet run

Looks like this:

![Terminalprompt](/doc/promt_thomprog.jpg "Terminalprompt")
![Terminalprompt](/doc/promt_thomprog1.jpg "Terminalprompt")

This starts the frontend you can now access the frontend via the following URL:

    http://localhost:5148/

![Frontend](/doc/frontend.jpg "Frontend")

You can log in with the following credentials:

    Username: demo
    Password: demo12345!

![Login](/doc/login.jpg "Login")

Then you will see the following:
![Thomprog](/doc/thomprog.jpg "Thomprog")

- Now you can start to create your own ToDo's.
- Record a Voice Message and save it. 
- Upload a Picture or a File and save it.
- Edit a HTML Text and save it.
- Manage Users and Roles.


## More Links to get inspired
I used the following Librarys to create the Thomprog
- [pocketbase-csharp-sdk](https://github.com/PRCV1/pocketbase-csharp-sdk)

- [MudBlazor](https://mudblazor.com/) 
- [Pocketbase](https://pocketbase.io/).  

I got some inspiration from the following links:
[Fullstackhero](https://fullstackhero.net/ )

I learnd a lot from the following links:
[Julio Casal](https://juliocasal.com/ )

