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
            <h1 class="text-center m-3">Campeões</h1>
            <div id="form">
                <div class="input-group mb-3">
                    <input type="text" v-model="champName" class="form-control text-white bg-dark border-1 border-secondary shadow-none" placeholder="Pesquise um campeão"/>
                    <button class="input-group-text btn btn-primary rounded-left border-1" @click="getChamp()"><i class="bi bi-search"></i></button>
                </div>
            </div>
            <div v-if="champByName !== null">
                <div class="card text-center m-2 bg-black" style="width: 9rem; height: 21rem;">
                        <img class="card-img-top" :src="champByName.squareImage" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">{{ champByName.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champByName.tags">
                                <span v-if="c == 'Mage'" class="badge bg-info">{{ c }}</span>
                                <span v-if="c == 'Assassin'" class="badge bg-danger">{{ c }}</span>
                                <span v-if="c == 'Support'" class="badge bg-warning">{{ c }}</span>
                                <span v-if="c == 'Tank'" class="badge bg-success">{{ c }}</span>
                                <span v-if="c == 'Marksman'" class="badge bg-white text-black">{{ c }}</span>
                                <span v-if="c == 'Fighter'" class="badge bg-primary">{{ c }}</span>
                            </div>
                            <a href="#" class="btn d-block btn-primary mt-1"><i class="bi bi-plus-circle"></i></a>
                        </div>
                    </div>
            </div>
            <div v-if="champByName === null" class="d-lg-flex flex-wrap d-sm-block justify-content-center">
                <div v-for="(champ, i) in champions" :key="i">
                    <div class="card text-center m-2 bg-black" style="width: 9rem; height: 21rem;">
                        <img class="card-img-top" :src="champ.squareImage" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">{{ champ.name }}</h5>
                            <div class="d-inline-block p-1" v-for="c in champ.tags">
                                <span v-if="c == 'Mage'" class="badge bg-info">{{ c }}</span>
                                <span v-if="c == 'Assassin'" class="badge bg-danger">{{ c }}</span>
                                <span v-if="c == 'Support'" class="badge bg-warning">{{ c }}</span>
                                <span v-if="c == 'Tank'" class="badge bg-success">{{ c }}</span>
                                <span v-if="c == 'Marksman'" class="badge bg-white text-black">{{ c }}</span>
                                <span v-if="c == 'Fighter'" class="badge bg-primary">{{ c }}</span>
                            </div>
                            <a href="#" class="btn d-block btn-primary mt-1"><i class="bi bi-plus-circle"></i></a>
                        </div>
                    </div>
                </div>
                <div v-if="allChampions.length > champions.length" class="m-4">
                    <button class="btn btn-success" @click="loadMore()">
                        Ver mais
                    </button>
                </div>
            </div>
        </div>
    </main>
</template>