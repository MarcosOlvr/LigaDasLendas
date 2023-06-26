<script lang="ts">
    import { defineComponent, onMounted } from "vue";
    import api from "../services/api";

    export default defineComponent({
        data() {
            return {
                spells: [],
            };
        },
        mounted() {
            api.get(`/spells`)
            .then((response) => (this.spells = response.data));
        },
});
</script>

<template>
    <div class="container">
        <h1 class="text-center m-3 text-warning">Feitiços de Invocador</h1>
        <div v-if="spells != null" class="row mb-2">
            <div class="col-sm" v-for="(spell, i) in spells" :key="i">
                <div class="card bg-dark text-center p-2 m-2 border-bottom border-secondary" style="width: 15rem; height: 30rem;">
                    <div>
                        <img class="img-fluid shadow rounded" :src="spell.image" height="75" width="75">    
                    </div>
                    <div class="card-body">
                        <h5 class="card-title text-warning">{{ spell.name }}</h5>
                        <p class="badge bg-info text-dark">{{ spell.cooldown }}s</p>
                        <p class="card-text">{{ spell.description }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
