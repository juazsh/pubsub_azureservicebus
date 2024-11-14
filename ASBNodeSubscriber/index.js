const { ServiceBusClient } = require("@azure/service-bus");

const connectionString = "";
const queueName = "asbqueue";

async function receive() {
  const sbClient = new ServiceBusClient(connectionString);
  const receiver = sbClient.createReceiver(queueName);

  console.log(`Listening for messages on queue: ${queueName}`);

  receiver.subscribe({
    processMessage: async (message) => {
      console.log(`Message Received, Message is : ${message.body}`);
    },
    processError: async (err) => {
      console.error("Error processing message: ", err);
    }
  });

  await new Promise((resolve) => setTimeout(resolve, Infinity));
}

receive().catch((err) => console.error("Error receiving messages", err))