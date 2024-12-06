
    //const connection = new signalR.HubConnectionBuilder()
    //.withUrl(`/chatHub`) // Match the hub URL in Program.cs
    //.build();

    //    connection.on("ReceiveMessage", (userId, message) => {
    //    addMessageToChat({ userName: userId, content: message, timestamp: new Date() });
    //    });

    //connection.start()
    //        .then(() => console.log("SignalR Connected"))
    //        .catch(err => console.error("Error connecting to SignalR:", err));

    

    //// Function to add a message to the chat
    //function addMessageToChat(message) {
    //        const messageList = document.getElementById('message-list');
    //const li = document.createElement('li');
    //li.innerHTML = `<strong>${message.userName}:</strong> ${message.content}<br /><small>${message.timestamp.toLocaleString()}</small>`;
    //messageList.appendChild(li);

    //// Scroll to the bottom
    //messageList.scrollTop = messageList.scrollHeight;
    //    }