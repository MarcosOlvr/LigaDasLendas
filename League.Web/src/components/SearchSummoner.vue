<script lang="ts">
    import { defineComponent } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        name: "SearchSummoner",
        setup() {
            const summoner = {};
            const summonerName = "";
            const summonerIconId = ""; 
            const summonerIconUrl = ""; 

            return { summoner, summonerName, summonerIconUrl };
        },
        methods: {
            getSummoner() {
                api.get(`/summoner/${this.summonerName}`)
                .then((response) => (this.summoner = response.data, this.summonerIconId = response.data.profileIconId));
            },
            getIcon() {
                api.get(`/icon/${this.summonerIconId}`)
                .then((response) => (this.summonerIconUrl = response.data));
            },
        },
    });
    </script>
    
    <template>
        <main>
            <div>
                <div id="form">
                    <input type="text" v-model="summonerName">
                    <button class="btn btn-primary" @click="getSummoner(); getIcon();">Buscar</button>
                </div>
                <p>{{ summoner }}</p>
                <img @src="summonerIconUrl" alt="asdkasmdklmas">
            </div>
        </main>
    </template>