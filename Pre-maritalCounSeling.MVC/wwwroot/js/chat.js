document.addEventListener("DOMContentLoaded", function () {
    const chatBox = document.getElementById("chatBox");
    const chatToggleBtn = document.getElementById("chatToggleBtn");
    const closeChatBtn = document.getElementById("closeChatBtn");
    const chatInput = document.getElementById("chatInput");
    const sendChatBtn = document.getElementById("sendChatBtn");
    const chatMessages = document.getElementById("chatMessages");

    // Toggle chat box visibility
    chatToggleBtn.addEventListener("click", function () {
        chatBox.style.display = chatBox.style.display === "block" ? "none" : "block";
        loadMessages(); // Load messages when opened
    });

    // Close chat box
    closeChatBtn.addEventListener("click", function () {
        chatBox.style.display = "none";
    });

    // Send message
    sendChatBtn.addEventListener("click", function () {
        sendMessage();
    });

    // Allow Enter key to send message
    chatInput.addEventListener("keypress", function (e) {
        if (e.key === "Enter") {
            sendMessage();
        }
    });

    function sendMessage() {
        let message = chatInput.value.trim();
        if (message === "") return;

        fetch("/Chat/SendMessage", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: `message=${encodeURIComponent(message)}`
        })
            .then(response => response.text())
            .then(html => {
                chatMessages.innerHTML = html;
                chatInput.value = "";
                chatMessages.scrollTop = chatMessages.scrollHeight; // Auto-scroll to latest message
            })
            .catch(error => console.error("Error:", error));
    }

    function loadMessages() {
        fetch("/Chat/ChatBox")
            .then(response => response.text())
            .then(html => {
                chatMessages.innerHTML = html;
                chatMessages.scrollTop = chatMessages.scrollHeight;
            })
            .catch(error => console.error("Error:", error));
    }
});
