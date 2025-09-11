mergeInto(LibraryManager.library, {
  sockets: [], 
  handler: "",

  WebSocketInit: function (handler) {
    handler = UTF8ToString(handler);
    this.handler = handler;

    // Inizializza l'array sockets se non è già stato fatto
    if (!this.sockets) {
      this.sockets = [];
    }

    console.log("Handler: " + this.handler);
  },

  WebSocketOpen: function (url) {
    url = UTF8ToString(url);

    console.log(url);

    const socket = new WebSocket(url);

    const socketIndex = this.sockets.length;

    this.sockets.push(socket);

    socket.addEventListener("open", (event) => {
      SendMessage(this.handler, "SocketOpenEventHandler", socketIndex);
    });

    socket.addEventListener("close", (event) => {
      SendMessage(this.handler, "SocketCloseEventHandler", socketIndex);
    });

    socket.addEventListener("message", (event) => {
      let responseContent;

      // binary frame: convert to string
      if (event.data instanceof ArrayBuffer) {
        const bufView = new Uint8Array(event.data); // Usa `event.data` anziché `buffer`
        responseContent = String.fromCharCode.apply(null, bufView);
      }
      // text frame
      else {
        responseContent = event.data;
      }

      SendMessage(
        this.handler,
        "SocketMessageEventHandler",
        socketIndex + "|" + responseContent
      );
    });

    socket.addEventListener("error", (event) => {
      SendMessage(this.handler, "SocketErrorEventHandler", socketIndex);
    });

    return socketIndex;
  },

  WebSocketSend: function (socketIndex, message) {
    message = UTF8ToString(message);

    if (socketIndex < 0 || socketIndex >= this.sockets.length) {
      console.error("Invalid socket index: ", socketIndex);
      return;
    }

    try {
      const socket = this.sockets[socketIndex];
      if (socket) {
        socket.send(message);
      }
    } catch (error) {
      console.error(error);
    }
  },

  WebSocketClose: function (socketIndex) {

    if (socketIndex < 0 || socketIndex >= this.sockets.length) {
      console.error("Invalid socket index: ", socketIndex);
      return;
    }

    const socket = this.sockets[socketIndex];
    if (socket) {
      socket.close();
    } else {
      console.error("Socket not found");
    }
  },
});
