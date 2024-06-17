<template>
    <div class="card flex justify-content-center">
        <Button label="Show" @click="visible = true"> Add Todo</Button>
        <Dialog v-model:visible="visible" modal header="Todo Addition" :style="{ width: '25rem' }">
            <div class="align-items-center p">
                <label for="todo" style="padding-right:5px">Todo</label>
                <Input type="text" ref="todo"/>
            </div>
            <div class="align-items-center">
                <label for="datetime" style="padding-right:5px">Date</label>
                <input type="datetime-local" ref="date" :value="defaultDate"/>
            </div>
            <div class="justify-content-end" style="padding:5px">
                <Button type="button" style="padding:5px" label="Cancel" severity="secondary" @click="visible = false">Cancel</Button>
                <Button type="button" style="padding:5px" label="Save" @click="SaveTodo()">Save</Button>
            </div>
        </Dialog>
    </div>
</template>

<script setup>
    import axios from 'axios';
    import { ref } from 'vue';
    import moment from 'moment';

    const emit = defineEmits(['saved'])
    const visible = ref(false);
    const todo = ref();
    const date = ref();
    const defaultDate = moment(String(new Date())).format('YYYY-MM-DDThh:mm');

    async function SaveTodo() {
        const response = await axios.post('http://localhost:42264/Planner/SaveTodoStickyNote', {
            Message: this.todo.value,
            Date: this.date.value
        })

        if (response.status == '200') {
            console.log(response)
            emit('saved');
            console.log("Done Successfully");
        }
        this.visible = false;
    }
</script>

<style>
    button {
        font: inherit;
        border: 1px solid #0076bb;
        background-color: #0076bb;
        color: white;
        cursor: pointer;
        padding: 0.5rem 0.5rem;
        border-radius: 30px;
    }

    button:hover,
    button:active {
        border-color: #002350;
        background-color: white;
    }
</style>
