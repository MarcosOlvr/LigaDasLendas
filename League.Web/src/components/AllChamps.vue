<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import api from "../services/api";  

export default defineComponent({
    name: "AllChamps",
    setup() {
        const champions = ref([]);

        const fetchChamp = async () => api.get("/champ/0/18")
        .then((response) => (champions.value = response.data.data));

        onMounted(fetchChamp);

        return { champions };
    },
});
</script>

<template>
    <main>
        <div class="container">
            <h1>All champs</h1>
            <div class="d-lg-flex flex-wrap d-sm-block">
                <div v-for="champ in champions">
                    <div class="card text-center m-2" style="width: 8rem;">
                        <img class="card-img-top" :src="champ.squareImage" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">{{ champ.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champ.tags">
                                <span class="badge bg-secondary">{{ c }}</span>
                            </div>
                            <a href="#" class="btn d-block btn-primary mt-1"><i class="bi bi-plus-circle"></i></a>
                        </div>
                    </div>
                </div>
            </div>
            <p>carregar mais</p>
        </div>
    </main>
</template>