<script >
export default {
  name: "AddRecipeOptionsView",
  data() {
    return {
      instructions: [],
      instructionNumber: 0
    }
  },
  mounted() {

  },
  methods: {
    // saveRecipe() {

    // },
    addInstruction() {
      let extraInstructionsDiv = document.getElementById('extraInstructions')
      let instruction = document.createElement("textarea")
      let label = document.createElement("label")
      instruction.setAttribute("id", "instruction" + this.instructionNumber)
      instruction.setAttribute("name", "instruction" + this.instructionNumber)
      instruction.setAttribute("rows", 1)
      label.setAttribute("for", "instruction")
      label.innerText = "Next Instruction: "
      extraInstructionsDiv.appendChild(label)
      extraInstructionsDiv.appendChild(instruction)
      let nextLine = document.createElement("br")
      extraInstructionsDiv.appendChild(nextLine)
      this.instructionNumber++
    }
  }
}
</script>

<template>
  <div class="div-main">
    <h2>Create/Add Your Own Recipes To Food Vault</h2>
    <br />
    <label for="recipeName">Name of Recipe: </label>
    <input type="text" id="recipeName" v-model="recipeName" />
    <br /><br />
    <label for="instructionOne"><b>Instructions: </b></label>
    <label for="instruction">First Instruction: </label>
    <textarea id="instruction" name="instruction" rows="1"></textarea>
    <br /><br />
    <div id="extraInstructions"></div>
    <br /><br />
    <button type="submit" id="addInstruction" @click="addInstruction">Add Another Instruction</button>
    <br /><br />
    <button type="submit" id="saveRecipe" @click="saveRecipe">Save Recipe</button>
  </div>
</template>

<style scoped>
.div-main {
  text-align: center;
}
</style>