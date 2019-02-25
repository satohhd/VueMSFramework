<template>
    <div class="d-inline">
        <!--<label :class="field.name" :for="field.name"><span v-html="field.caption" /></label>-->
        <b-table v-if="viewModel[field.name]"
                 :id="_id"
                 :class="field.name" hover table-responsive
                 :items="viewModel[field.name]"
                 :fields="field.childFields"
                 :sort-by="field.sortBy"
                 :sort-desc="field.sortDesc">
            <!-- 共通部品だけど、業務固有の項目記載が必要になってしまう -->
            <template alt="選択" title="選択" slot="isSelect" slot-scope="row">
                <b-button v-if="field.readonly" :pressed="row.item.isSelect" variant="outline-success" @click.stop.prevent="return false">
                    {{ row.item.isSelect==true?"レ":"　" }}
                </b-button>
                <b-button v-else :pressed.sync="row.item.isSelect" variant="outline-success" key="outline-success">
                    {{ row.item.isSelect==true?"レ":"　" }}
                </b-button>
            </template>

            <!--<span slot="kntiChart" slot-scope="data" v-html="data.value" :class="data.item.dow"></span>-->
            <span slot="knmkbn_KnmkbnJsk" slot-scope="data" v-html="data.value"></span>
            <span slot="sykjk_SykjkDkk" slot-scope="data" v-html="data.value"></span>
            <span slot="tkjk_TkjkDkk" slot-scope="data" v-html="data.value"></span>

            <span slot="kykJkn" slot-scope="data" v-html="data.value"></span>
            <span slot="zgyjkn" slot-scope="data" v-html="data.value"></span>
            <span slot="zgyjkn36" slot-scope="data" v-html="data.value"></span>

            <template slot="kntiChart" slot-scope="row">
                <ae-chart-bar :startTime="row.item.sykjk" :endTime="row.item.tkjk" :restTime="row.item.kykJkn" :overTime="row.item.zgyjkn" :startTime2="row.item.sykjkDkk" :endTime2="row.item.tkjkDkk"></ae-chart-bar>
            </template>
            <template slot="examineeId" slot-scope="row">
                <a @click.stop.prevent="listener(section,row.field,row.item)">{{row.value}}</a>
            </template>
            <template slot="outsidePublicLink" slot-scope="row">
                <a :href="row.value" target="_blank">{{row.value}}</a>
            </template>
            <template slot="created" slot-scope="row">
                {{ row.value == null ? null : $moment(row.value).fromNow() }}
            </template>
            <template slot="sentDate" slot-scope="row">
                {{ row.value == null ? null : $moment(row.value).fromNow() }}
            </template>
            <template slot="btnApply" slot-scope="row">
                <button title="申請" class="btn btn-outline-danger" variant="outline-primary" @click.stop.prevent="listener(section,row.field,row.item)">申請</button>
            </template>
            <template slot="btnEdit" slot-scope="row">
                <img alt="編集" title="編集" style="width:25px" src="images/edit.png" @click.stop.prevent="listener(section,row.field,row.item)" />
            </template>
            <template slot="btnRefer" slot-scope="row">
                <img alt="参照" title="参照" style="width:25px" src="images/refer.png" @click.stop.prevent="listener(section,row.field,row.item)" />
            </template>
            <template slot="btnRemove" slot-scope="row">
                <img alt="削除" title="削除" style="width:25px" src="images/trash.png" @click.stop.prevent="listener(section,row.field,row.item)" />
            </template>
        </b-table>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        methods: {
            /* ******************************************************************
             *
             * listener
             *
             * param  string           section
             * param  {}               field{action}
             * param  {}               viewModel{..}
             * $emit  teller
             *
             ******************************************************************* */
            listener: function (section, field, param) {
                //alert(JSON.stringify(param))
                this.$emit('teller', section, field, param)
            },

        },
    }

</script>
<style scoped>
</style>
