<script lang="ts">
    import { defineComponent, onMounted } from "vue";
    import api from "../services/api";

    export default defineComponent({
        data() {
            return {
                rune: {},
            };
        },
        mounted() {
            api.get(`/runes/${this.$route.params.runeName}`)
            .then((response) => (this.rune = response.data));
        },
});
</script>

<template>
    <div class="container">
        <div v-if="rune != null">
            <div class="text-center">
                <img class="rounded" :src="rune.icon" height="50">
                <h1 class="text-center m-3 text-warning">{{ rune.name }}</h1>
                <div class="row">
                    <div class="col-md-4 p-3" v-for="(slot, i) in rune.slots" :key="i">
                        <div class="card-group">
                            <div class="card bg-dark p-2 border-secondary">
                                <div class="text-center">
                                    <img class="rounded" :src="slot.icon" height="55" width="55">
                                </div>
                                <div class="card-body">
                                    <h5 class="card-title text-warning">{{ slot.name }}</h5>
                                    <p class="card-text">{{ slot.description }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
