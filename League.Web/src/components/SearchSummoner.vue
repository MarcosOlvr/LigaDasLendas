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
        </main>
    </template>