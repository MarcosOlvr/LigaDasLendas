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
            latestMatches: [],
            items: []
        };
    },
    methods: {
        search() {
            api.get(`/summoner/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (this.summoner = response.data));
            
            api.get(`/summoner/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (api.get(`/icon/${response.data.profileIconId}`)
            .then((response) => (this.summonerIconUrl = response.data))));

            api.get(`/masteries/${this.summonerName}`)
            .then((response) => (this.masteries = response.data));

            api.get(`summoner/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (api.get(`/league/${response.data.id}`)
            .then((response) => (this.league = response.data[0]))));

            api.get(`summoner/${this.summonerName}-${this.riotTagLine}`)
            .then((response) => (api.get(`/match/latest/${response.data.puuid}`)
            .then((response) => (response.data.forEach(element => {
                api.get(`match/${element}`)
                .then((response => (this.latestMatches.push(response.data.info)))
                )})
            ))));
        },
        getItem(item0, item1, item2, item3, item4, item5) {
            api.get(`/items?item1=${item0}
            &item2=${item1}
            &item3=${item2}
            &item4=${item3}
            &item5=${item4}
            &item6=${item5}`).then((response) => (this.items.push(response.data.data)))
        }
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
                            <div class="mt-2">
                                <h3 class="text-warning">{{ summoner.name }}</h3>
                                <span class="mx-2 mt-2">
                                    <span class="badge bg-info text-black">{{ summoner.summonerLevel }}</span>
                                </span>
                            </div>
                        </div>
                        <div v-if="league !== undefined" class="col-4 text-center border-end border-secondary">
                            <img :src="'src/services/imagens/rank/' + league.tier + '.png'" height="125"/>
                            <div class="d-flex justify-content-center mt-2"> 
                                <p>{{ league.tier }}</p>
                                <p class="ms-1">{{ league.rank }}</p>
                            </div>
                            <span class="badge bg-warning">{{ league.leaguePoints }} pdl</span>
                        </div>
                        <div v-else class="col-4 text-center border-end border-secondary">
                            <img src="../services/imagens/rank/unranked.png" height="125">
                        </div>
                        <div class="col-4">
                            <div class="d-inline-block p-3 p-2 text-center" v-for="m in masteries">
                                <div>
                                    <img class="shadow" :src="m.champion.squareImage" height="75">
                                    <h6 class="mt-2 text-warning">{{ m.champion.name }}</h6>
                                    <img :src="'https://raw.githubusercontent.com/InFinity54/LoL_DDragon/master/extras/masteries/mastery' + m.championLevel + '.png'" height="55">
                                    <p>
                                        <span class="badge bg-info text-black">{{ m.championPoints }} pts</span> 
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <div v-if="latestMatches !== null">
                        <div v-for="(match, i) in latestMatches.sort((a, b) => a.gameId - b.gameId ).reverse()" :key="i">
                            <div v-for="player in match.participants">
                                <div v-if="player.gameEndedInEarlySurrender === false">
                                    <div v-if="player.win">
                                        <div class="row border border-info p-2 m-2" v-if="player.summonerName === summonerName">
                                            <div class="col-2 text-center">
                                                <p class="text-warning" v-if="match.queueId === 420">Partida ranqueada</p>
                                                <p class="text-warning" v-else-if="match.queueId === 400">Partida normal</p>
                                                <p class="text-warning" v-else-if="match.queueId === 440">Partida ranqueada flex</p>
                                                <p class="text-warning" v-else-if="match.queueId === 450">ARAM</p>
                                                <p class="text-warning" v-else-if="match.queueId === 1900">URF</p>
                                                <span v-if="player.win === true" class="badge bg-info text-black">VITÓRIA</span>
                                                <span v-else class="badge bg-danger">DERROTA</span>
                                            </div>
                                            <div class="col-5 text-center">
                                                <h3>{{ player.championName }}</h3>
                                                <span>{{ player.kills }} / <span class="text-danger">{{ player.deaths }}</span>  / {{ player.assists }}</span>
                                                <p v-if="player.challenges.kda >= 0" class="badge bg-warning m-2">KDA {{ player.challenges.kda.toFixed(2) }}</p>
                                            </div>
                                            <div class="col">
                                                <button class="btn btn-primary" @click="getItem(player.item0, player.item1, player.item2, player.item3, player.item4, player.item5)">teste</button>   
                                                <div v-if="items !== null">
                                                    <div v-for="(item, i) in items" :key="i">
                                                        <p>{{ item }}</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div v-else>
                                        <div class="row border border-danger p-2 m-2" v-if="player.summonerName === summonerName">
                                            <div class="col-2 text-center">
                                                <p class="text-warning" v-if="match.queueId === 420">Partida ranqueada</p>
                                                <p class="text-warning" v-else-if="match.queueId === 400">Partida normal</p>
                                                <p class="text-warning" v-else-if="match.queueId === 440">Partida ranqueada flex</p>
                                                <p class="text-warning" v-else-if="match.queueId === 450">ARAM</p>
                                                <p class="text-warning" v-else-if="match.queueId === 1900">URF</p>
                                                <span class="badge bg-danger">DERROTA</span>
                                            </div>
                                            <div class="col-5 text-center">
                                                <h3>{{ player.championName }}</h3>
                                                <span>{{ player.kills }} / <span class="text-danger">{{ player.deaths }}</span>  / {{ player.assists }}</span>
                                                <p v-if="player.challenges.kda >= 0" class="badge bg-warning m-2">KDA {{ player.challenges.kda.toFixed(2) }}</p>  
                                            </div>
                                            <div class="col">
                                                {{ player.item0 }}
                                                {{ player.item1 }}
                                                {{ player.item2 }}
                                                {{ player.item3 }}
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