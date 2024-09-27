import { HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { defineStore } from "pinia";
import { InvitationRequest } from "../requests/invitation-request";
import { useAccountStore } from "./accountStore";
import { ref } from 'vue'
import { useInvitationsStore } from "./invitationsStore";

export const useSignalrStore = defineStore("signalr", () => {
  const connection = ref();

  const delay = ms => new Promise(res => setTimeout(res, ms));

  const connectToAllGroups = async (groupIds) => {
    let retryCount = 0
    while (connection.value.state !== HubConnectionState.Connected) {
      await delay(2000)
      if (retryCount > 5)
        return // abort if can't connect
      retryCount++
    }

    for (let i = 0; i < groupIds.length; i++) {
      connection.value.invoke("AddClientToGroup", groupIds[i]);
    }
  }

  const startConnection = async () => {
    console.log('starting connection')
    connection.value = new HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_FOODVAULT_SERVER}/GroupHub`, { accessTokenFactory: () => useAccountStore().activeToken })
      .withAutomaticReconnect()
      .build();
    await connection.value.start().catch((err) => console.log('error caught'))

    connection.value.on("InviteReceived", async () => {
      await useInvitationsStore().getInvitations(useAccountStore().currentUserId)
    })
  }

  return {
    connectToAllGroups,
    startConnection
  }
})