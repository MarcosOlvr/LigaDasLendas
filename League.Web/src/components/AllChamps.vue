<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import api from "../services/api";  

export default defineComponent({
    name: "AllChamps",
    setup() {
        const champions = ref([]);

        const fetchChamp = async () => api.get("/champ/all")
        .then((response) => (champions.value = response.data));

        onMounted(fetchChamp);

        return { champions };
    },
});
</script>

<template>
    <main>
        <div>
            <h1>All champs</h1>
            <div>
                <ul>
                    <li v-for="champ in champions">
                        {{ champ.name }}
                        <img :src="champ.squareImage" alt="" height="50">
                    </li>
                </ul>
            </div>
        </div>
    </main>
</template>