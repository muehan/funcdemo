<template>
  <div class="container">
    <h1>Start working</h1>

    <div class="input-group mb-3">
      <div class="input-group-prepend">
        <span class="input-group-text" id="todo-id">new-todo</span>
      </div>
      <input
        type="text"
        class="form-control"
        placeholder="todo"
        aria-label="todo"
        aria-describedby="todo-id"
        v-model="newtodo"
        @keyup.enter="trigger"
      />

      <div class="input-group-append">
        <button
          class="btn btn-outline-secondary"
          type="button"
          id="button-addon2"
          v-on:click="submit()"
          ref="createTodo"
        >Add</button>
      </div>
    </div>

    <div class="todos">
      <div class="custom-control custom-checkbox todo" v-for="todo in todos" :key="todo.id">
        <input type="checkbox" class="custom-control-input" v-bind:id="todo.id" v-bind:checked="todo.isCompleted" v-on:click="update(todo.id, !todo.isCompleted)" />
        <label class="custom-control-label" v-bind:for="todo.id">{{ todo.taskDescription }}</label>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import Resource from "vue-resource";
Vue.use(Resource);

export default {
  name: "Todos",
  props: {},
  data() {
    return {
      todos: [],
      newtodo: ""
    };
  },
  mounted() {
    this.loadTodos();
  },
  methods: {
    loadTodos() {
      this.$http
        .get(process.env.VUE_APP_API_ROOT + "/api/todo")
        .then(response => {
          this.todos = response.body;
        });
    },
    submit() {
      if (this.newtodo != "") {
        this.$http
          .post(process.env.VUE_APP_API_ROOT + "/api/todo", {
            taskDescription: this.newtodo
          })
          .then(_response => {
            this.newtodo = "";
            this.loadTodos();
          });
      }
    },
    trigger() {
      this.$refs.createTodo.click();
    },
    update(id, checked) {
      console.log(id + checked);
      this.$http
          .put(process.env.VUE_APP_API_ROOT + "/api/todo/" + id, {
            isCompleted: checked
          })
          .then(_response => {
            this.loadTodos();
          });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1 {
  margin: 40px 0 0;
}

.todos {
  margin-top: 20px;
}

.todo {
  margin: 5px;
  text-align: left;
}
</style>
