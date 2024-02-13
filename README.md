# Thomprog  :construction:
This programme was created purely out of curiosity about how to create a frontend with [MudBlazor](https://mudblazor.com/) and [Pocketbase](https://pocketbase.io/). As I couldn't find anything on the net that met my requirements, I ended up programming something myself. The Thomprog is available to anyone who wants to continue using it. Documentation will follow here shortly. 
## Start Backend :crystal_ball:
  

Change to the pocketbase directory in the terminal and execute the following command:

    pocketbase.exe serve

This starts the backend

├─ REST API: http://127.0.0.1:8090/api/

└─ Admin UI: http://127.0.0.1:8090/_/

Looks like this:

![Terminalprompt](/doc/promt_pb.jpg "Terminalprompt")

For more information, see Readme in Pocketbase folder.

## Start Frontend :computer:

In the thomprog directory run:

    dotnet run

Looks like this:

![Terminalprompt](/doc/promt_thomprog.jpg "Terminalprompt")
![Terminalprompt](/doc/promt_thomprog1.jpg "Terminalprompt")

This starts the frontend you can now access the frontend via the following URL:

    http://localhost:5148/

![Frontend](/doc/frontend.jpg "Frontend")

You can log in with the following credentials:

    Admin
    Username: demo
    Password: demo12345!

    User
    Username: user
    Password: user12345!

![Login](/doc/login.jpg "Login")

Then you will see the following:
![Thomprog](/doc/thomprog.jpg "Thomprog")

Now you can start to create your ToDo's.
![ToDo](/doc/todo.jpg "ToDo")

Test a Write a Message. 
![Chat](/doc/chat.jpg "Chat")

Record a Voice Message and save it.
![Record](/doc/recorder.jpg "Record")

Upload a Picture or a File and save it.
![Fileupload](/doc/fileuplaod.jpg "Fileupload")

Edit a HTML Text and save it.
![Editor](/doc/editor.jpg "Editor")

Manage Users and Roles.
![User](/doc/user.jpg "User")

At the moment all User sees the documents of all other users.

## More Links to get inspired
I used the following Librarys to create the Thomprog
- [pocketbase-csharp-sdk](https://github.com/PRCV1/pocketbase-csharp-sdk)

- [MudBlazor](https://mudblazor.com/) 
- [Pocketbase](https://pocketbase.io/).  

I got some inspiration from the following links:
[Fullstackhero](https://fullstackhero.net/ )
[namin-amin/pocketbase.NET](https://github.com/namin-amin/pocketbase.NET/tree/master/uitest/Pages )

I learnd a lot from the following links:
[Julio Casal](https://juliocasal.com/ )


## Support my work :heart:
If you like my work and want to support me, I would be very happy about a small donation. 
<a href="https://www.buymeacoffee.com/potenzialentwickler" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>