<template>
    <div v-for="day in days">
        <Card class="card">
            <template #title>
                {{ day }}
            </template>
            <template #content>
                <div v-for="todo in todos">
                    <div v-if="showStickyNotForDate(day, todo.date)">
                        <StickyNote :message="todo.message"></StickyNote>
                    </div>
                </div>
            </template>
        </Card>
    </div>
</template>

<script setup lang="ts">
    import { PropType, onMounted, ref, watch } from "vue"
    import axios from 'axios';
    import TodoForm from './TodoForm.vue';
    import StickyNote from './StickyNote.vue';
    import moment from 'moment';

    const props = defineProps({
        days: {
            type: Array as PropType<string>
        },
        todos: {
            type: Array as PropType<TodoMessages[]>
        }
    });

    interface TodoMessages {
        Id: number,
        TodoMessage: String,
        DateTodo: Date
    }

    function showStickyNotForDate(day: String, date: String): Boolean {
        // needed the conversion cause the date return from dackend is Mvc format which cant match
        return moment(String(day)).format('M/DD/YYYY') == moment(String(date)).format('M/DD/YYYY');
    }
</script>

<style scoped>
    .card{
        height: 100vh;
    }
</style>