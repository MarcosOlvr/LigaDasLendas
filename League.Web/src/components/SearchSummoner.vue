<script lang="ts">
    import { defineComponent } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        data() {
        return {
            summonerName: "",
            summoner: null,
            summonerIconUrl: "",
            masteries: [],
            league: [],
        };
    },
    methods: {
        search() {
            api.get(`/summoner/${this.summonerName}`)
            .then((response) => (this.summoner = response.data));
            
            api.get(`/summoner/${this.summonerName}`)
            .then((response) => (api.get(`/icon/${response.data.profileIconId}`)
            .then((response) => (this.summonerIconUrl = response.data))));

            api.get(`/masteries/${this.summonerName}`)
            .then((response) => (this.masteries = response.data));

            api.get(`summoner/${this.summonerName}`)
            .then((response) => (api.get(`/league/${response.data.id}`)
            .then((response) => (this.league = response.data[0]))));
        },
    },
    });
    </script>
    
    <template>
        <main class="container">
            <div>
                <div v-if="summoner !== null">
                    <div class="row">
                        <div class="col-4 text-center border-end border-secondary">
                            <img class="m-3 rounded shadow" :src="summonerIconUrl" height="100">
                            <div class="d-flex justify-content-center mt-2">
                                <h3 class="text-warning">{{ summoner.name }}</h3>
                                <span class="mx-3 mt-2">
                                    Nível <span class="text-info">{{ summoner.summonerLevel }}</span>
                                </span>
                            </div>
                        </div>
                        <div v-if="league !== null" class="col-4 text-center border-end border-secondary">
                            <img v-if="league.tier === 'IRON'" src="../services/imagens/IRON.png" height="125">
                            <img v-if="league.tier === 'BRONZE'" src="../services/imagens/BRONZE.png" height="125">
                            <img v-if="league.tier === 'SILVER'" src="../services/imagens/SILVER.png" height="125">
                            <img v-if="league.tier === 'GOLD'" src="../services/imagens/GOLD.png" height="125">
                            <img v-if="league.tier === 'PLATINUM'" src="../services/imagens/PLATINUM.png" height="125">
                            <img v-if="league.tier === 'DIAMOND'" src="../services/imagens/DIAMOND.png" height="125">
                            <img v-if="league.tier === 'MASTER'" src="../services/imagens/MASTER.png" height="125">
                            <img v-if="league.tier === 'GRANDMASTER'" src="../services/imagens/GRANDMASTER.png" height="125">
                            <img v-if="league.tier === 'CHALLENGER'" src="../services/imagens/CHALLENGER.png" height="125">
                            <div class="d-flex justify-content-center mt-2"> 
                                <p>{{ league.tier }}</p>
                                <p class="ms-1">{{ league.rank }}</p>
                            </div>
                            <span class="text-white-50">{{ league.leaguePoints }} pdl</span>
                        </div>
                        <div class="col-4">
                            <div class="d-inline-block p-3 p-2 text-center" v-for="m in masteries">
                                <div>
                                    <img class="shadow" :src="m.champion.squareImage" height="75">
                                    <h6 class="mt-2 text-warning">{{ m.champion.name }}</h6>
                                    <span class="text-info">{{ m.championLevel }}</span>
                                    <p>
                                        <span class="text-muted">{{ m.championPoints }} pts</span> 
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                </div>
            </div>
            <div v-if="summoner === null">
                <img src="../services/imagens/download.jpg" width="500" class="mx-auto d-block mt-4 mb-3 img-responsive shadow">
                <div class="input-group mb-3">
                    <input type="text" v-model="summonerName" class="form-control text-white bg-dark border-1 border-secondary shadow" placeholder="Nome de Invocador"/>
                    <button class="input-group-text btn btn-primary rounded-left border-1" @click="search()"><i class="bi bi-search"></i></button>
                </div>
            </div> 
        </main>
    </template>