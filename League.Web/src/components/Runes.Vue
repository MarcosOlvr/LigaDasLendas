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
            <div class="col-sm m-3" v-for="(rune, i) in runes" :key="i">
                <div class="card text-center bg-dark p-2" style="width: 14rem;">
                    <div class="shadow">
                        <img class="img-fluid rounded" :src="rune.icon" alt="Card image cap" height="40">
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">{{ rune.name }}</h5>
                        <router-link class="link-info" :to="{ name: 'rune', params: { runeName: rune.name }}">
                            Ver árvore de runas
                        </router-link>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
