document.addEventListener("DOMContentLoaded", function () {
    const chatToggleBtn = document.getElementById("chatToggleBtn");
    const chatBox = document.getElementById("chatBox");
    const closeChatBtn = document.getElementById("closeChatBtn");
    const chatInput = document.getElementById("chatInput");
    const sendChatBtn = document.getElementById("sendChatBtn");
    const chatMessages = document.getElementById("chatMessages");

    // Toggle chat box visibility
    chatToggleBtn.addEventListener("click", function () {
        chatBox.style.display = chatBox.style.display === "block" ? "none" : "block";
    });

    closeChatBtn.addEventListener("click", function () {
        chatBox.style.display = "none";
    });

    // Send chat message
    sendChatBtn.addEventListener("click", sendMessage);
    chatInput.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            sendMessage();
        }
    });

    function sendMessage() {
        const message = chatInput.value.trim();
        if (message === "") return;


        fetch("/Chat/SendMessage", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ message })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    chatMessages.innerHTML = ""; // Clear existing messages
                    data.messages.forEach(msg => addMessage(msg)); // Update messages
                    chatInput.value = "";
                }
            })
            .catch(error => console.error("Error:", error));


        //fetch("/Chat/SendMessage", {
        //    method: "POST",
        //    headers: {
        //        "Content-Type": "application/json"
        //    },
        //    body: JSON.stringify({ message })
        //})
        //    .then(response => response.json())
        //    .then(data => {
        //        if (data.success) {
        //            addMessage(message);
        //            chatInput.value = "";
        //        }
        //    })
        //    .catch(error => console.error("Error:", error));
    }

    function addMessage(message) {
        const messageDiv = document.createElement("div");
        messageDiv.classList.add("chat-message");
        messageDiv.textContent = message;
        chatMessages.appendChild(messageDiv);
    }
});