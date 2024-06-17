<template>
    <div>
        <h1 style="text-align:center">Planner</h1>
    </div>
    <TodoForm @saved="SavedTodo"></TodoForm>
    <div style="align-content:end; position:absolute">
        <label for="datetime" style="padding-right:5px;background-color:white">Date</label>
        <input type="datetime-local" ref="dateSelect" :value="defaultDate" :onchange="dateChange"/>
    </div>
    <div class="container">
        <DayCard :days="days"
                 :todos="todaysTodos">
        </DayCard>
    </div>
</template>

<script setup lang="ts">
    import DayCard from './DayCard.vue';
    import TodoForm from './TodoForm.vue';
    import { ref, onMounted } from 'vue';
    import axios from 'axios';
    import moment from 'moment';

    let today = new Date();
    let tomorrow = createTomorrow(today);
    let yesterday = createYesterday(today);
    let dateSelect = ref();
    let days = ref([yesterday.toLocaleDateString(), today.toLocaleDateString(), tomorrow.toLocaleDateString()]);

    const defaultDate = moment(String(today)).format('YYYY-MM-DDThh:mm');
    const todoToday = ref();
    const todaysTodos = ref<TodoMessages[]>();

    interface TodoMessages {
        Id: number,
        TodoMessage: String,
        DateTodo: Date
    }

    function SavedTodo() {
        location.reload();
    }

    async function dateChange() {
        const daySelected = dateSelect.value.value;
        let date = new Date(daySelected);
        const dayBefore = createYesterday(date);
        const dayAfter = createTomorrow(date);
        days.value = [dayBefore.toLocaleDateString(), date.toLocaleDateString(), dayAfter.toLocaleDateString()];
        await getTodos(daySelected);
        console.log(days.value);
        console.log(todaysTodos.value);
    }

    function createTomorrow(date: Date): Date {
        var datetime = new Date();
        console.log(date);
        datetime.setDate(date.getDate() + 1);
        return datetime;
    }

    function createYesterday(date: Date): Date {
        var datetime = new Date();
        datetime.setDate(date.getDate() - 1);
        return datetime;
    }

    onMounted(async () => {
        await getTodos(today);
    })

    async function getTodos(date: String) {
        let day = moment(String(date)).format('YYYY-MM-DDThh:mm');
        const response = await axios.post('http://localhost:42264/Planner/GetTodoForDay', {
            Date: day
        });

        if (response.status == '200') {
            todaysTodos.value = response.data;
        }
    }
</script>

<style scoped>
    .container {
        column-count: 3;
        column-gap: 40px;
        padding: 40px;
    }
</style>