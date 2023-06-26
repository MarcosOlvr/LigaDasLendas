<script lang="ts">
    import { defineComponent, onMounted } from "vue";
    import api from "../services/api";

    export default defineComponent({
        data() {
            return {
                champion: null,
            };
        },
        mounted() {
            api.get(`/champ/${this.$route.params.champName}`)
            .then((response) => (this.champion = response.data));
        },
});
</script>

<template>
    <div class="container">
        <div v-if="champion != null" class="row">
            <div class="col-6 text-center">
                <img class="img-fluid shadow rounded" height="400" :src="champion.loadScreenImage">
            </div>
            <div class="col-6">
                <h1 class="text-warning">{{ champion.name }}</h1>
                <h6 class="text-info">{{ champion.title }}</h6>
                <hr>
                <p>{{ champion.lore }}</p>
                <div class="d-inline-block p-2" v-for="c in champion.tags">
                    <span v-if="c == 'Mage'" class="badge bg-info text-black p-2">{{ c }}</span>
                    <span v-if="c == 'Assassin'" class="badge bg-danger p-2">{{ c }}</span>
                    <span v-if="c == 'Support'" class="badge bg-warning p-2">{{ c }}</span>
                    <span v-if="c == 'Tank'" class="badge bg-success p-2">{{ c }}</span>
                    <span v-if="c == 'Marksman'" class="badge bg-white text-black p-2">{{ c }}</span>
                    <span v-if="c == 'Fighter'" class="badge bg-primary p-2">{{ c }}</span>
                </div>
            </div>
            <hr class="mt-3">
            <div class="row mb-3">
                <div class="mt-3 col-sm border-start border-secondary" v-for="(skill, i) in champion.skills" :key="i">
                    <div class="text-center p-2" style="width: 20rem;">
                        <img class="img-fluid shadow rounded" height="80" :src="skill.image" alt="image of skill">
                        <div class="mt-2">
                            <h5 class="text-warning">{{ skill.name }}</h5>
                            <p>{{ skill.description }}</p>
                        </div>
                    </div>
                </div>
            </div>
            <hr>
            <div class="row mb-3">
                <div class="mt-3 col-4" v-for="(skin, i) in champion.skins" :key="i">
                    <div class="text-center border-bottom border-end border-secondary p-2">
                        <img class="img-fluid shadow rounded" :src="skin.skinImage">
                        <h5 class="text-warning p-2" v-if="skin.name === 'default'">Padrão</h5>
                        <h5 class="text-warning p-2 mt-2" v-else>{{ skin.name }}</h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
