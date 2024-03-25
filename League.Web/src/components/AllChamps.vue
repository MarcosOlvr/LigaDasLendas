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
            <h1 class="text-center m-3 text-warning">Campeões</h1>
            <div id="form">
                <div class="input-group mb-3">
                    <input type="text" v-model="champName" class="form-control text-white bg-dark border-1 border-secondary shadow-none" placeholder="Pesquise um campeão"/>
                    <button class="input-group-text btn btn-primary rounded-left border-1" @click="getChamp()"><i class="bi bi-search"></i></button>
                </div>
            </div>
            <div v-if="champByName !== null">
                <div class="card text-center m-2 bg-black" style="width: 9rem; height: 21rem;">
                        <img class="card-img-top" :src="champByName.squareImage">
                        <div class="card-body">
                            <h5 class="card-title">{{ champByName.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champByName.tags">
                                <span v-if="c == 'Mage'" class="badge bg-info text-black">{{ c }}</span>
                                <span v-if="c == 'Assassin'" class="badge bg-danger">{{ c }}</span>
                                <span v-if="c == 'Support'" class="badge bg-warning">{{ c }}</span>
                                <span v-if="c == 'Tank'" class="badge bg-success">{{ c }}</span>
                                <span v-if="c == 'Marksman'" class="badge bg-white text-black">{{ c }}</span>
                                <span v-if="c == 'Fighter'" class="badge bg-primary">{{ c }}</span>
                            </div>
                            <router-link :to="{ name: 'champion', params: { champName: champByName.name }}" class="d-block mt-2"><i class="bi bi-plus-circle"></i></router-link>
                        </div>
                    </div>
            </div>
            <div v-if="champByName === null" class="d-lg-flex flex-wrap d-sm-block justify-content-center">
                <div v-for="(champ, i) in champions" :key="i">
                    <div class="card text-center m-2 bg-black" style="width: 8rem; height: 18rem;">
                        <img class="card-img-top" :src="champ.squareImage">
                        <div class="card-body">
                            <h5 class="card-title">{{ champ.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champ.tags">
                                <span v-if="c == 'Mage'" class="badge bg-info text-black">{{ c }}</span>
                                <span v-if="c == 'Assassin'" class="badge bg-danger">{{ c }}</span>
                                <span v-if="c == 'Support'" class="badge bg-warning">{{ c }}</span>
                                <span v-if="c == 'Tank'" class="badge bg-success">{{ c }}</span>
                                <span v-if="c == 'Marksman'" class="badge bg-white text-black">{{ c }}</span>
                                <span v-if="c == 'Fighter'" class="badge bg-primary">{{ c }}</span>
                            </div>
                            <router-link :to="{ name: 'champion', params: { champName: champ.name }}" class="d-block mt-2"><i class="bi bi-plus-circle"></i></router-link>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="allChampions.length > champions.length && champByName === null" class="m-4 text-center">
                    <button class="btn btn-primary" @click="loadMore()">
                        Ver mais
                    </button>
            </div>
        </div>
    </main>
</template>