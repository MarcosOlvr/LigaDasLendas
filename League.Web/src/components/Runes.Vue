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
                <div class="card text-center bg-dark p-2" style="width: 18rem;">
                    <div>
                        <img class="img-fluid shadow rounded" :src="rune.icon" alt="Card image cap" height="50" width="50">
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">{{ rune.name }}</h5>
                        <a class="link-info" href="#">Ver árvore de runas</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
