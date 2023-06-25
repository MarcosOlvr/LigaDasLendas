<script lang="ts">
    import { defineComponent, onMounted } from "vue";
    import api from "../services/api";

    export default defineComponent({
        data() {
            return {
                runes: [],
            };
        },
        mounted() {
            api.get(`/runes`)
            .then((response) => (this.runes = response.data));
        },
});
</script>

<template>
    <div class="container">
        <div v-if="runes != null" class="row">
            <div class="col-sm" v-for="(rune, i) in runes" :key="i">
                <div class="card text-center bg-dark" style="width: 18rem;">
                    <img class="card-img-top" :src="rune.icon" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">{{ rune.name }}</h5>
                        <a href="#" class="btn btn-primary">Mais informações</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
