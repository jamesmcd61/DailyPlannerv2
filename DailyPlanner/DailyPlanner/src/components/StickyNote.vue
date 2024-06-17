<template>
    <Fieldset legend="Sticky Note" :toggleable="true">
        <p class="m-0">
            {{ props.message }}
        </p>
    </Fieldset>
</template>

<script setup lang="ts">
    import { PropType } from "vue"
    import Fieldset from 'primevue/fieldset';

    const props = defineProps({
        message: {
            type: String
        }
    });
</script>

<style scoped>
</style>