<script lang="ts">
    import { defineComponent } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        data() {
        return {
            summonerName: "",
            riotTagLine: "",
            summoner: null,
            summonerIconUrl: "",
            masteries: [],
            league: {},
            latestMatches: {},
        };
    },
    methods: {
        search() {
            api.get(`/summoner/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.summoner = response.data));
            
            api.get(`/icon/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.summonerIconUrl = response.data));

            api.get(`/masteries/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.masteries = response.data));

            api.get(`/league/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.league = response.data[0]));

            api.get(`/match/latest/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.latestMatches = response.data));
        }
    },
    });
    </script>
    
    <template>
        <main class="container">
            <div>
                <div v-if="summoner !== null">
                    <div class="row align-items-center">
                        <div class="col text-center border-end border-secondary">
                            <img class="rounded shadow" :src="summonerIconUrl" height="100">
                            <div>
                                <h3 class="text-warning">{{ summoner.name }}</h3>
                                <span>
                                    <span class="badge bg-info text-black">{{ summoner.summonerLevel }}</span>
                                </span>
                            </div>
                        </div>
                        <div v-if="league !== undefined" class="col-4 text-center border-end border-secondary">
                            <img :src="'src/services/imagens/rank/' + league.tier + '.png'" height="125"/>
                            <div class="d-flex justify-content-center"> 
                                <p>{{ league.tier }}</p>
                                <p class="ms-1">{{ league.rank }}</p>
                            </div>
                            <span class="badge bg-warning">{{ league.leaguePoints }} pdl</span>
                        </div>
                        <div v-else class="col text-center border-end border-secondary">
                            <img src="../services/imagens/rank/unranked.png" height="125">
                        </div>
                        <div class="col text-center">
                            <div class="d-inline-block px-3 text-center" v-for="m in masteries">
                                <div>
                                    <img class="shadow" :src="m.champion.squareImage" height="75">
                                    <h6 class="text-warning">{{ m.champion.name }}</h6>
                                    <img v-if="m.championLevel <= 10" :src="'https://raw.githubusercontent.com/InFinity54/LoL_DDragon/master/extras/championmastery/levels/mastery-' + m.championLevel + '.png'" height="55">
                                    <img v-else :src="'https://raw.githubusercontent.com/InFinity54/LoL_DDragon/master/extras/championmastery/levels/mastery-10.png'" height="55">
                                    <p>
                                        <span class="badge bg-info text-black">{{ m.championPoints }} pts</span> 
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <div class="row justify-content-center">
                        <div v-if="latestMatches !== null" class="col-10">
                            <div v-for="(match, key) in latestMatches" :key="key">
                                <div v-for="m in match.matchList">
                                    <div v-for="player in m.participants">
                                        <div v-if="player.gameEndedInEarlySurrender === false">
                                            <div>
                                                <div v-bind:style= "[player.win ? {'border': 'solid #2387FA'} : {'border': 'solid #EB1A01'}]" class="row p-2 m-2 align-items-center" v-if="player.summonerId === summoner.id">
                                                    <div class="col-3 text-center">
                                                        <p class="text-warning" v-if="m.queueId === 420">Partida ranqueada</p>
                                                        <p class="text-warning" v-else-if="m.queueId === 400">Partida normal</p>
                                                        <p class="text-warning" v-else-if="m.queueId === 440">Partida ranqueada flex</p>
                                                        <p class="text-warning" v-else-if="m.queueId === 450">ARAM</p>
                                                        <p class="text-warning" v-else-if="m.queueId === 1900">URF</p>
                                                        <span v-if="player.win === true" class="badge bg-info text-black">VITÓRIA</span>
                                                        <span v-else class="badge bg-danger">DERROTA</span>
                                                    </div>
                                                    <div class="col-5 text-center">
                                                        <h3>{{ player.championName }}</h3>
                                                        <span>{{ player.kills }} / <span class="text-danger">{{ player.deaths }}</span>  / {{ player.assists }}</span>
                                                        <p v-if="player.challenges.kda !== null" class="badge bg-warning m-2">KDA {{ player.challenges.kda.toFixed(2) }}</p>
                                                        <p v-else></p>
                                                    </div>
                                                    <div class="col-4 text-center">
                                                        <div v-for="item in latestMatches[key].itemList" class="d-inline-block border border-secondary">
                                                            <img class="m-1 rounded shadow" :src='`${item.image}`' height="28">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="summoner === null">
                <img src="../services/imagens/bg/download.jpg" class="mx-auto d-block mt-4 mb-3 img-fluid shadow" width="500">
                <div class="input-group mb-3 row">
                    <div class="col-8">
                        <input type="text" v-model="summonerName" class="form-control text-white bg-dark border-1 border-secondary shadow" placeholder="Nome de Invocador"/>
                    </div>
                    <div class="col-3">
                        <input type="text" v-model="riotTagLine" class="form-control text-white bg-dark border-1 border-secondary shadow" placeholder="BR1"/>
                    </div>
                    <div class="col-1">
                        <button class="input-group-text btn btn-primary rounded-left border-1" @click="search()"><i class="bi bi-search"></i></button>
                    </div>
                </div>
                <span class="text-muted text-center">Região: Apenas Brasil</span>
            </div> 
        </main>
    </template>