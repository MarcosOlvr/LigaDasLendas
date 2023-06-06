<script lang="ts">
    import { defineComponent } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        data() {
        return {
            summonerName: "",
            summoner: null,
            summonerIconUrl: "",
        };
    },
    methods: {
        search() {
            api.get(`/summoner/${this.summonerName}`)
            .then((response) => (this.summoner = response.data));
            
            api.get(`/summoner/${this.summonerName}`)
            .then((response) => (api.get(`/icon/${response.data.profileIconId}`)
            .then((response) => (this.summonerIconUrl = response.data))));
        },
    },
    });
    </script>
    
    <template>
        <main class="container">
            <div>
                <div v-if="summoner !== null">
                    <div class="d-inline-block">
                        <img class="m-3 rounded" :src="summonerIconUrl" height="100">
                        <div class="d-flex">
                            <h3>{{ summoner.name }}</h3>
                            <span class="mx-3 mt-2">
                                Nível <span class="text-warning">{{ summoner.summonerLevel }}</span>
                            </span>
                        </div>
                    </div>
                    <hr>
                </div>
            </div>
            <div v-if="summoner === null">
                <img src="../services/imagens/download.jpg" width="500" class="mx-auto d-block mt-4 mb-3 img-responsive">
                <div class="input-group mb-3">
                    <input type="text" v-model="summonerName" class="form-control text-white bg-dark border-1 border-secondary shadow-none" placeholder="Nome de Invocador"/>
                    <button class="input-group-text btn btn-primary rounded-left border-1" @click="search()"><i class="bi bi-search"></i></button>
                </div>
            </div> 
        </main>
    </template>