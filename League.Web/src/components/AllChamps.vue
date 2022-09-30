<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import api from "../services/api";  

export default defineComponent({
    data() {
        return {
            champName: "",
            champByName: null,
            champions: [],
            allChampions: [],
        };
    },
    mounted() {
        api.get("/champ/0/16")
        .then((response) => (this.champions = response.data.data));

        api.get("/champ/0/300")
        .then((response) => (this.allChampions = response.data.data));
    },
    methods: {
        loadMore() {
            api.get(`/champ/0/${Number.parseInt(this.champions.length) + 16}`)
            .then((response) => (this.champions = response.data.data));
        },
        getChamp() {
            api.get(`/champ/${this.champName}`)
            .then((response) => (this.champByName = response.data));
        }
    },
});
</script>

<template>
    <main>
        <div class="container">
            <h1>All champs</h1>
            <div id="form">
                <input type="text" v-model="champName">
                <button @click="getChamp();">Buscar</button>
            </div>
            <div v-if="champByName !== null">
                <div class="card text-center m-2" style="width: 9rem;">
                        <img class="card-img-top" :src="champByName.squareImage" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">{{ champByName.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champByName.tags">
                                <span class="badge bg-secondary">{{ c }}</span>
                            </div>
                            <a href="#" class="btn d-block btn-primary mt-1"><i class="bi bi-plus-circle"></i></a>
                        </div>
                    </div>
            </div>
            <div v-if="champByName === null" class="d-lg-flex flex-wrap d-sm-block">
                <div v-for="(champ, i) in champions" :key="i">
                    <div class="card text-center m-2" style="width: 9rem;">
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
                <div v-if="allChampions.length > champions.length">
                    <button class="btn btn-success" @click="loadMore()">
                        Carregar mais
                    </button>
            </div>
            </div>
        </div>
    </main>
</template>