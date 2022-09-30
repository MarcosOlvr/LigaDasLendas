<script lang="ts">
    import { defineComponent } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        data() {
        return {
            summonerName: "",
            summoner: {},
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
        <main>
            <div id="form">
                <input type="text" v-model="summonerName">
                <button @click="search();">Buscar</button>
            </div>
            <div v-if="summoner !== null">
                <img :src="summonerIconUrl" height="100">
                <h3>{{ summoner.name }}</h3>
            </div>
        </main>
    </template>