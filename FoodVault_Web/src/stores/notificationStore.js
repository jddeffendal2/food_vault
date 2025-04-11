import { defineStore } from 'pinia'
import { ref } from 'vue'
import { v4 as generateId } from 'uuid'

export const useNotificationStore = defineStore('notification', () => {
  const notifications = ref([])

  /**
   * 
   * @param {string} type: success, warning, error, info
   * @param {string} title
   * @param {string} text
   */
  const createNotification = (type, title, text) => {
    const notificationId = generateId()
    notifications.value.push({
      id: notificationId,
      visible: true,
      type,
      title,
      text
    })

    const notificationLifetime = 10 * 1000

    setTimeout(() => {
      // Hide notification after 10 seconds
      const notificationToRemove = notifications.value.find(x => x.id === notificationId)
      if (notificationToRemove) {
        notificationToRemove.visible = false
      }
      
      // Wait 1 second after notification is hidden to actually remove it from the array
      setTimeout(() => {
        notifications.value = notifications.value.filter(x => x.id !== notificationId)
      }, 1000)
    }, notificationLifetime)
  }

  return { notifications, createNotification }
})